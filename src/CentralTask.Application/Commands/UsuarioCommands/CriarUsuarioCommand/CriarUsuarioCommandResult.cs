using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.UsuarioCommands.CriarUsuarioCommand;

public class CriarUsuarioCommandResult : CommandResult
{
    public Guid Id { get; set; }
}