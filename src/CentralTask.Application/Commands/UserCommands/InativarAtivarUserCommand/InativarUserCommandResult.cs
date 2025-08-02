using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.UserCommands.InativarAtivarUserCommand;

public class InativarUserCommandResult : CommandResult
{
    public Guid Id { get; set; }
}