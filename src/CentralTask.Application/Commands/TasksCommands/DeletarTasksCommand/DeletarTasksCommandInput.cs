
using CentralTask.Core.Mediator.Commands;
using CentralTask.Domain.Entidades;

namespace CentralTask.Application.Commands.TasksCommands
{
    public class DeletarTasksCommandInput : CommandInput<DeletarTasksCommandResult>
    {
        public Guid Id { get; set; }
    }
}
