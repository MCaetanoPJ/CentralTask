using AutoMapper;
using CentralTask.Core.Mediator.Commands;
using CentralTask.Core.Notifications;
using CentralTask.Domain.Entidades;
using CentralTask.Domain.Enums;
using CentralTask.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CentralTask.Application.Commands.UserCommands.InativarAtivarUserCommand;

public class InativarUserCommandHandler : ICommandHandler<InativarUserCommandInput, InativarUserCommandResult>
{
    private readonly ILogger<InativarUserCommandHandler> _logger;
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _UserRepository;
    private readonly INotifier _notifier;
    private readonly IMapper _mapper;

    public InativarUserCommandHandler(
        ILogger<InativarUserCommandHandler> logger,
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

    public async Task<InativarUserCommandResult> Handle(InativarUserCommandInput request, CancellationToken cancellationToken)
    {
        var inativarUser = _UserRepository.Get().FirstOrDefault(x => x.Id == request.Id);

        if (inativarUser != null)
        {
            if (inativarUser.Active == Status.Ativo)
                inativarUser.Active = Status.Inativo;
            else
                inativarUser.Active = Status.Ativo;

            _UserRepository.Update(inativarUser);

            await _UserRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new InativarUserCommandResult { Id = inativarUser.Id };
        }

        _notifier.Notify($"Não foi localizado Usuário no nosso sistema.");
        return new InativarUserCommandResult();
    }
}
