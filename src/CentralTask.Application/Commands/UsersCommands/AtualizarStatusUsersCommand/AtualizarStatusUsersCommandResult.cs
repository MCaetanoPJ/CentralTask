
using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.UsersCommands
{
    public class AtualizarStatusUsersCommandResult : CommandResult
    {
        public Guid? Id { get; set; }
    }
}