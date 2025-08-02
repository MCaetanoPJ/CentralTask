
using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.TasksCommands
{
    public class AlterarTasksCommandInput : CommandInput<AlterarTasksCommandResult>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Duedate { get; set; }
        public Guid Userid { get; set; }
        public DateTime Createdat { get; set; }
    }
}
