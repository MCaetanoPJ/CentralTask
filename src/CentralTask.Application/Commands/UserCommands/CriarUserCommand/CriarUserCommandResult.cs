using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.UserCommands.CriarUserCommand;

public class CriarUserCommandResult : CommandResult
{
    public Guid Id { get; set; }
}