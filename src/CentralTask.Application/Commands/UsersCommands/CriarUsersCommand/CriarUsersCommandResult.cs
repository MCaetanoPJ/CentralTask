
using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.UsersCommands
{
    public class CriarUsersCommandResult : CommandResult
    {
        public Guid? Id { get; set; }
    }
}