
using CentralTask.Core.Mediator.Commands;
using CentralTask.Domain.Entidades;

namespace CentralTask.Application.Commands.UsersCommands
{
    public class DeletarUsersCommandInput : CommandInput<DeletarUsersCommandResult>
    {
        public Guid Id { get; set; }
    }
}
