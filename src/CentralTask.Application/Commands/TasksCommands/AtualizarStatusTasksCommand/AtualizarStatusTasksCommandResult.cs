
using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.TasksCommands
{
    public class AtualizarStatusTasksCommandResult : CommandResult
    {
        public Guid? Id { get; set; }
    }
}