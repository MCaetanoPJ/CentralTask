
using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.UsersCommands
{
    public class DeletarUsersCommandResult : CommandResult
    {
        public Guid? Id { get; set; }
    }
}