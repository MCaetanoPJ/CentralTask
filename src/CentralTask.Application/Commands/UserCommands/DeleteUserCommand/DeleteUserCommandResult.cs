using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.UserCommands.DeleteUserCommand;

public class DeleteUserCommandResult : CommandResult
{
    public Guid Id { get; set; }
}