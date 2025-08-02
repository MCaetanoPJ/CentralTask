using CentralTask.Core.Mediator.Commands;
using CentralTask.Core.Notifications;
using CentralTask.Application.Services.Interfaces;

namespace CentralTask.Application.Commands.UsuarioCommands.DeleteUsuarioCommand;

public class DeleteUsuarioCommandHandler : ICommandHandler<DeleteUsuarioCommandInput, DeleteUsuarioCommandResult>
{
    private readonly IUSuarioService _usuarioService;
    private readonly INotifier _notifier;

    public DeleteUsuarioCommandHandler(
        IUSuarioService usuarioService,
        INotifier notifier)
    {
        _usuarioService = usuarioService;
        _notifier = notifier;
    }

    public async Task<DeleteUsuarioCommandResult> Handle(DeleteUsuarioCommandInput request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            _notifier.Notify($"Nenhum dado recebido.");
            return new DeleteUsuarioCommandResult();
        }

        if (request.Id == new Guid() || request.Id == Guid.Empty)
        {
            _notifier.Notify($"Id do usuário informado não é válido.");
            return new DeleteUsuarioCommandResult();
        }

        var validacao = await _usuarioService.DeletarUsuario(request.Id);
        if (!validacao.Sucess)
        {
            _notifier.Notify(string.Join(",", validacao.Messages));
            return new DeleteUsuarioCommandResult { Id = request.Id };
        }

        return new DeleteUsuarioCommandResult();
    }
}
