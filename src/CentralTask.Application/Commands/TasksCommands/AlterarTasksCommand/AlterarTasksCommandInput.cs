
using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.TasksCommands
{
    public class AlterarTasksCommandInput : CommandInput<AlterarTasksCommandResult>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Guid UserId { get; set; }
    }
}
