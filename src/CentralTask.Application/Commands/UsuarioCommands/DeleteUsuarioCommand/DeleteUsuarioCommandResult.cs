using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.UsuarioCommands.DeleteUsuarioCommand;

public class DeleteUsuarioCommandResult : CommandResult
{
    public Guid Id { get; set; }
}