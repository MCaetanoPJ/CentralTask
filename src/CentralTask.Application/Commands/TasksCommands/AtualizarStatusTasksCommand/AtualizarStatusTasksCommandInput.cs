
using CentralTask.Core.Mediator.Commands;
using CentralTask.Domain.Enums;

namespace CentralTask.Application.Commands.TasksCommands
{
    public class AtualizarStatusTasksCommandInput : CommandInput<AtualizarStatusTasksCommandResult>
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
    }
}
