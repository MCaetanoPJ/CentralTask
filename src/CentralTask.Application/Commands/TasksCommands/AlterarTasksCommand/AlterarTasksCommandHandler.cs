
using CentralTask.Core.Mediator.Commands;
using CentralTask.Core.Notifications;
using CentralTask.Domain.Entidades;
using CentralTask.Domain.Interfaces.Repositories;

namespace CentralTask.Application.Commands.TasksCommands
{
    public class AlterarTasksCommandHandler : ICommandHandler<AlterarTasksCommandInput, AlterarTasksCommandResult>
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly INotifier _notifier;

        public AlterarTasksCommandHandler(ITasksRepository tasksRepository, INotifier notifier)
        {
            _tasksRepository = tasksRepository;
            _notifier = notifier;
        }

        public async Task<AlterarTasksCommandResult> Handle(AlterarTasksCommandInput request, CancellationToken cancellationToken)
        {
            var entidade = new Tasks
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                UserId = request.UserId,
            };

            _tasksRepository.Update(entidade);

            await _tasksRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new AlterarTasksCommandResult { Id = entidade.Id };
        }
    }
}