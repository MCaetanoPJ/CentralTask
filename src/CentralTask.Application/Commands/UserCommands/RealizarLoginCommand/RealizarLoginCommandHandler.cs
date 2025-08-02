using CentralTask.Application.Services.Interfaces;
using CentralTask.Core.Mediator.Commands;
using CentralTask.Core.Notifications;
using CentralTask.Core.Settings;
using CentralTask.Domain.Entidades;
using CentralTask.Domain.Enums;
using CentralTask.Domain.Interfaces.Repositories;
using ChoETL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CentralTask.Application.Commands.UserCommands.RealizarLoginCommand;

public class RealizarLoginCommandHandler : ICommandHandler<RealizarLoginCommandInput, RealizarLoginCommandResult>
{
    private readonly ILogger<RealizarLoginCommandHandler> _logger;
    private readonly IUserRepository _UserAppRepository;
    private readonly IUserRepository _UserRepository;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly INotifier _notifier;
    private readonly JwtSettings _jwtSettings;
    private readonly IUserService _UserService;

    public RealizarLoginCommandHandler(
        ILogger<RealizarLoginCommandHandler> logger,
        IUserRepository UserAppRepository,
        IUserRepository UserRepository,
        SignInManager<User> signInManager,
        UserManager<User> userManager,
        IOptions<JwtSettings> jwtOptions,
        IConfiguration configuration,
        INotifier notifier,
        IUserService UserService)
    {
        _logger = logger;
        _UserAppRepository = UserAppRepository;
        _UserRepository = UserRepository;
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtSettings = jwtOptions.Value;
        _configuration = configuration;
        _notifier = notifier;
        _UserService = UserService;
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

        var User = _UserRepository.Get().Where(x => x.Email.ToLower().Trim() == email).FirstOrDefault();

        if (User != null)
        {
            if (User.Status != Status.Ativo)
            {
                _notifier.Notify("Usuário não cadastrado.");
                return new RealizarLoginCommandResult();
            }
        }

        _UserRepository.Update(User);
        await _UserRepository.UnitOfWork.SaveChangesAsync();

        _logger.LogInformation("Usuário {UserId} logado com sucesso.", User.Id);

        return await GerarReponseComToken(User);
    }

    public async Task<RealizarLoginCommandResult> GerarReponseComToken(User User)
    {
        var claims = new Claim[]
               {
                    new Claim("role", User.NivelAcesso.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, User.Id.ToString())
               };

        var encodedToken = CriarToken(claims, User.NivelAcesso);
        var responses = new RealizarLoginCommandResult
        {
            AccessToken = encodedToken,
            ExpiresInSeconds = TimeSpan.FromHours(_jwtSettings.ExpiracaoHoras).TotalSeconds,
            Nivel = ((int)User.NivelAcesso),
            Email = User.Email,
            Nome = User.Nome,
            UserId = User.Id,
        };

        return responses;
    }

    private string CriarToken(IEnumerable<Claim> claims, EnumNivel nivelUser)
    {
        var tempoDeExpiracaoDoToken = ObterTempoDeExpiracao(nivelUser);

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