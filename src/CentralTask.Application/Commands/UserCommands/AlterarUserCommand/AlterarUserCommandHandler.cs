using AutoMapper;
using CentralTask.Core.Mediator.Commands;
using CentralTask.Core.Notifications;
using CentralTask.Domain.Entidades;
using CentralTask.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CentralTask.Application.Commands.UserCommands.AlterarUserCommand;

public class AlterarUserCommandHandler : ICommandHandler<AlterarUserCommandInput, AlterarUserCommandResult>
{
    private readonly ILogger<AlterarUserCommandHandler> _logger;
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _UserRepository;
    private readonly INotifier _notifier;
    private readonly IMapper _mapper;

    public AlterarUserCommandHandler(
        ILogger<AlterarUserCommandHandler> logger,
        UserManager<User> userManager,
        IUserRepository UserRepository,
        IMapper mapper,
        INotifier notifier)
    {
        _logger = logger;
        _userManager = userManager;
        _UserRepository = UserRepository;
        _mapper = mapper;
        _notifier = notifier;
    }

    public async Task<AlterarUserCommandResult> Handle(AlterarUserCommandInput request, CancellationToken cancellationToken)
    {
        var User = await _UserRepository
            .Get()
            .Where(c => c.Id.Equals(request.IdUser))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (User is null)
        {
            _notifier.Notify("User não encontrado.");
            return new();
        }

        if (request.Senha != null && request.Senha != string.Empty)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(User);

            var result = await _userManager.ResetPasswordAsync(User, token, request.Senha);
        }

        if (request.Email != null && request.Email != string.Empty)
        {
            User.Email = request.Email;
            User.NormalizedUserName = request.Email.ToUpper();
            User.NormalizedEmail = request.Email.ToUpper();
            User.UserName = request.Email.ToLower();
        }

        await _UserRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new AlterarUserCommandResult() { Id = User.Id };
    }
}