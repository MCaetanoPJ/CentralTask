using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.UserCommands.AlterarUserCommand;

public class AlterarUserCommandResult : CommandResult
{
    public Guid Id { get; set; }
}