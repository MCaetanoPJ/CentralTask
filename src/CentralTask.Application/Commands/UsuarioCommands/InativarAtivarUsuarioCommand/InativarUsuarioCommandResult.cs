using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.UsuarioCommands.InativarAtivarUsuarioCommand;

public class InativarUsuarioCommandResult : CommandResult
{
    public Guid Id { get; set; }
}