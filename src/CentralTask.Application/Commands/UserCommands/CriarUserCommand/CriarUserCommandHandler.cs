using CentralTask.Core.Mediator.Commands;
using CentralTask.Core.Notifications;
using CentralTask.Domain.Entidades;
using CentralTask.Domain.Enums;
using CentralTask.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace CentralTask.Application.Commands.UserCommands.CriarUserCommand;

public class CriarUserCommandHandler : ICommandHandler<CriarUserCommandInput, CriarUserCommandResult>
{
    private readonly ILogger<CriarUserCommandHandler> _logger;
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly INotifier _notifier;

    public CriarUserCommandHandler(
        ILogger<CriarUserCommandHandler> logger,
        UserManager<User> userManager,
        IUserRepository userRepository,
        INotifier notifier)
    {
        _logger = logger;
        _userManager = userManager;
        _userRepository = userRepository;
        _notifier = notifier;
    }

    public async Task<CriarUserCommandResult> Handle(CriarUserCommandInput request, CancellationToken cancellationToken)
    {
        if (await _userManager.Users.AnyAsync(c => c.Email == request.Email))
        {
            _notifier.Notify("Esse e-mail já está sendo utilizado por outro usuário.");
            return new CriarUserCommandResult();
        }

        _logger.LogInformation("Iniciando criação do User {EmailUser}", request!.Email);

        var User = new User(request.Nome, request.Email);

        var result = await _userManager.CreateAsync(User, request.Senha);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors) _notifier.Notify(error.Description);
            return new CriarUserCommandResult();
        }

        var levelAccess = EnumNivel.Admin.ToString();
        var rolesCurrentToUser = await _userManager.GetRolesAsync(User);
        if (!await _userManager.IsInRoleAsync(User, levelAccess))
        {
            var newRule = new Claim(ClaimTypes.Role, levelAccess);
        }

        var resultRole = await _userManager.AddToRoleAsync(User, levelAccess);

        if (!resultRole.Succeeded)
        {
            foreach (var error in resultRole.Errors) _notifier.Notify(error.Description);
            return new CriarUserCommandResult();
        }

        return new CriarUserCommandResult { Id = User.Id };
    }
}