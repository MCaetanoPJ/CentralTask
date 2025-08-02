
using CentralTask.Core.Mediator.Commands;
using CentralTask.Domain.Enums;

namespace CentralTask.Application.Commands.UsersCommands
{
    public class AtualizarStatusUsersCommandInput : CommandInput<AtualizarStatusUsersCommandResult>
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
    }
}
