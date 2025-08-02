
using CentralTask.Domain.Interfaces.Repositories;
using CentralTask.Core.Mediator.Commands;
using CentralTask.Core.Notifications;
using CentralTask.Domain.Entidades;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CentralTask.Application.Commands.TasksCommands
{
    public class DeletarTasksCommandHandler : ICommandHandler<DeletarTasksCommandInput, DeletarTasksCommandResult>
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly INotifier _notifier;

        public DeletarTasksCommandHandler(ITasksRepository tasksRepository, INotifier notifier)
        {
            _tasksRepository = tasksRepository;
            _notifier = notifier;
        }

        public async Task<DeletarTasksCommandResult> Handle(DeletarTasksCommandInput request, CancellationToken cancellationToken)
        {
            var entidade = _tasksRepository.Get().FirstOrDefault(c => c.Id == request.Id);
            
            _tasksRepository.Remove(entidade);
            await _tasksRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new DeletarTasksCommandResult();
        }
    }
}
