using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.UsuarioCommands.AlterarUsuarioCommand;

public class AlterarUsuarioCommandResult : CommandResult
{
    public Guid Id { get; set; }
}