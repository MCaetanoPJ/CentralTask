
using CentralTask.Domain.Interfaces.Repositories;
using CentralTask.Core.Mediator.Commands;
using CentralTask.Core.Notifications;
using CentralTask.Domain.Entidades;
using CentralTask.Core.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace CentralTask.Application.Commands.TasksCommands
{
    public class CriarTasksCommandHandler : ICommandHandler<CriarTasksCommandInput, CriarTasksCommandResult>
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly INotifier _notifier;

        public CriarTasksCommandHandler(ITasksRepository tasksRepository, INotifier notifier)
        {
            _tasksRepository = tasksRepository;
            _notifier = notifier;
        }

        public async Task<CriarTasksCommandResult> Handle(CriarTasksCommandInput request, CancellationToken cancellationToken)
        {
            var entidade = new Tasks
            {
                Title = request.Title,
                Description = request.Description,
                Duedate = request.Duedate,
                Userid = request.Userid,
                Createdat = request.Createdat,

            };

              _tasksRepository.Add(entidade);

            await _tasksRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new CriarTasksCommandResult { Id = entidade.Id };
        }
    }
}