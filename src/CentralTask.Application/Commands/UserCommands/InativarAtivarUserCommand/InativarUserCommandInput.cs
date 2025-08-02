using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.UserCommands.InativarAtivarUserCommand;

public class InativarUserCommandInput : CommandInput<InativarUserCommandResult>
{
    public Guid Id { get; set; }
}