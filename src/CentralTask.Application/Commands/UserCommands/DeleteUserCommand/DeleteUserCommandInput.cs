using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.UserCommands.DeleteUserCommand;

public class DeleteUserCommandInput : CommandInput<DeleteUserCommandResult>
{
    public Guid Id { get; set; }
}