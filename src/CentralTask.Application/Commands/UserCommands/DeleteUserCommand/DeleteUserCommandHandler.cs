using CentralTask.Application.Services.Interfaces;
using CentralTask.Core.Mediator.Commands;
using CentralTask.Core.Notifications;

namespace CentralTask.Application.Commands.UserCommands.DeleteUserCommand;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommandInput, DeleteUserCommandResult>
{
    private readonly IUserService _UserService;
    private readonly INotifier _notifier;

    public DeleteUserCommandHandler(
        IUserService UserService,
        INotifier notifier)
    {
        _UserService = UserService;
        _notifier = notifier;
    }

    public async Task<DeleteUserCommandResult> Handle(DeleteUserCommandInput request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            _notifier.Notify($"Nenhum dado recebido.");
            return new DeleteUserCommandResult();
        }

        if (request.Id == new Guid() || request.Id == Guid.Empty)
        {
            _notifier.Notify($"Id do usuário informado não é válido.");
            return new DeleteUserCommandResult();
        }

        var validacao = await _UserService.DeletarUser(request.Id);
        if (!validacao.Sucess)
        {
            _notifier.Notify(string.Join(",", validacao.Messages));
            return new DeleteUserCommandResult { Id = request.Id };
        }

        return new DeleteUserCommandResult();
    }
}
