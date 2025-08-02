using CentralTask.Core.Extensions;
using CentralTask.Core.Mediator.Commands;
using CentralTask.Core.Notifications;
using CentralTask.Domain.Entidades;
using CentralTask.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace CentralTask.Application.Commands.UsuarioCommands.CriarUsuarioCommand;

public class CriarUsuarioCommandHandler : ICommandHandler<CriarUsuarioCommandInput, CriarUsuarioCommandResult>
{
    private readonly ILogger<CriarUsuarioCommandHandler> _logger;
    private readonly UserManager<Usuario> _userManager;
    private readonly IUsuarioRepository _userRepository;
    private readonly INotifier _notifier;

    public CriarUsuarioCommandHandler(
        ILogger<CriarUsuarioCommandHandler> logger,
        UserManager<Usuario> userManager,
        IUsuarioRepository userRepository,
        INotifier notifier)
    {
        _logger = logger;
        _userManager = userManager;
        _userRepository = userRepository;
        _notifier = notifier;
    }

    public async Task<CriarUsuarioCommandResult> Handle(CriarUsuarioCommandInput request, CancellationToken cancellationToken)
    {
        request.Cpf = request.Cpf.ApenasNumeros();

        if (await _userManager.Users.AnyAsync(c => c.Cpf == request.Cpf))
        {
            _notifier.Notify("O CPF informado já foi cadastrado, informe outro.");
            return new CriarUsuarioCommandResult();
        }

        _logger.LogInformation("Iniciando criação do usuario {EmailUsuario}", request!.Email);

        var usuario = new Usuario(request.Nome, request.Sobrenome, request.Email, request.NivelAcesso);

        var result = await _userManager.CreateAsync(usuario, request.Senha);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors) _notifier.Notify(error.Description);
            return new CriarUsuarioCommandResult();
        }

        var rolesCurrentToUser = await _userManager.GetRolesAsync(usuario);

        try
        {
            if (!await _userManager.IsInRoleAsync(usuario, request.NivelAcesso.ToString()))
            {
                var newRule = new Claim(ClaimTypes.Role, request.NivelAcesso.ToString());
            }

            var resultRole = await _userManager.AddToRoleAsync(usuario, request.NivelAcesso.ToString());

            if (!resultRole.Succeeded)
            {
                foreach (var error in resultRole.Errors) _notifier.Notify(error.Description);
                return new CriarUsuarioCommandResult();
            }
        }
        catch (Exception ex)
        {
            var tt = ex.Message;
        }        

        return new CriarUsuarioCommandResult { Id = usuario.Id };
    }
}