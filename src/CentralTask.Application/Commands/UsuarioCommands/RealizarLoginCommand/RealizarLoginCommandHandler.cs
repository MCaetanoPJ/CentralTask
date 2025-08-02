using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CentralTask.Core.Mediator.Commands;
using CentralTask.Core.Notifications;
using CentralTask.Core.Settings;
using CentralTask.Domain.Entidades;
using CentralTask.Domain.Enums;
using CentralTask.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using CentralTask.Application.Services.Interfaces;
using ChoETL;

namespace CentralTask.Application.Commands.UsuarioCommands.RealizarLoginCommand;

public class RealizarLoginCommandHandler : ICommandHandler<RealizarLoginCommandInput, RealizarLoginCommandResult>
{
    private readonly ILogger<RealizarLoginCommandHandler> _logger;
    private readonly IUsuarioRepository _usuarioAppRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly UserManager<Usuario> _userManager;
    private readonly IConfiguration _configuration;
    private readonly INotifier _notifier;
    private readonly JwtSettings _jwtSettings;
    private readonly IUSuarioService _usuarioService;

    public RealizarLoginCommandHandler(
        ILogger<RealizarLoginCommandHandler> logger,
        IUsuarioRepository usuarioAppRepository,
        IUsuarioRepository usuarioRepository,
        SignInManager<Usuario> signInManager,
        UserManager<Usuario> userManager,
        IOptions<JwtSettings> jwtOptions,
        IConfiguration configuration,
        INotifier notifier,
        IUSuarioService usuarioService)
    {
        _logger = logger;
        _usuarioAppRepository = usuarioAppRepository;
        _usuarioRepository = usuarioRepository;
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtSettings = jwtOptions.Value;
        _configuration = configuration;
        _notifier = notifier;
        _usuarioService = usuarioService;
    }

    public async Task<RealizarLoginCommandResult> Handle(RealizarLoginCommandInput request, CancellationToken cancellationToken)
    {
        var email = request.Email.ToLower().Trim();

        var signInResult = await _signInManager.PasswordSignInAsync(email, request.Senha, false, false);

        if (!signInResult.Succeeded)
        {
            _notifier.Notify("E-mail ou senha incorretos.");
            return new RealizarLoginCommandResult();
        }

        var usuario = _usuarioRepository.Get().Where(x => x.Email.ToLower().Trim() == email).FirstOrDefault();

        if (usuario != null)
        {
            if (usuario.Status != Status.Ativo)
            {
                _notifier.Notify("Usuário não cadastrado.");
                return new RealizarLoginCommandResult();
            }
        }

        if (!string.IsNullOrEmpty(request.DeviceId) && usuario.DeviceId != request.DeviceId)
        {
            var usuariosComDeviceId = _usuarioAppRepository.GetAsNoTracking().Where(x => x.DeviceId == request.DeviceId).ToList();
            usuariosComDeviceId.ForEach(x => x.DeviceId = null);
            _usuarioAppRepository.UpdateRange(usuariosComDeviceId);

            usuario.DeviceId = request.DeviceId;
        }

        _usuarioRepository.Update(usuario);
        await _usuarioRepository.UnitOfWork.SaveChangesAsync();

        _logger.LogInformation("Usuário {UsuarioId} logado com sucesso.", usuario.Id);

        return await GerarReponseComToken(usuario);
    }

    public async Task<RealizarLoginCommandResult> GerarReponseComToken(Usuario usuario)
    {
        try
        {
            var claims = new Claim[]
           {
                new Claim("role", usuario.NivelAcesso.ToString()),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
           };

            var encodedToken = CriarToken(claims, usuario.NivelAcesso);

            var urlFoto = await _usuarioAppRepository.ObterFoto(usuario.Id);

            var responses = new RealizarLoginCommandResult
            {
                AccessToken = encodedToken,
                ExpiresInSeconds = TimeSpan.FromHours(_jwtSettings.ExpiracaoHoras).TotalSeconds,
                Nivel = ((int)usuario.NivelAcesso),
                Email = usuario.Email,
                Nome = usuario.Nome,
                UsuarioId = usuario.Id,
                FotoUrl = urlFoto,
            };

            return responses;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private string CriarToken(IEnumerable<Claim> claims, EnumNivel nivelUsuario)
    {

        var tempoDeExpiracaoDoToken = ObterTempoDeExpiracao(nivelUsuario);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey!);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = "CentralTask",
            Audience = "https://localhost.com",
            Subject = new ClaimsIdentity(claims),
            Expires = tempoDeExpiracaoDoToken,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encodedToken = tokenHandler.WriteToken(token);
        return encodedToken;
    }

    private static DateTime? ObterTempoDeExpiracao(EnumNivel nivelAcesso)
    {
        return nivelAcesso switch
        {
            EnumNivel.Admin => DateTime.Now.AddYears(1),

            _ => throw new NotImplementedException(),
        };
    }
}