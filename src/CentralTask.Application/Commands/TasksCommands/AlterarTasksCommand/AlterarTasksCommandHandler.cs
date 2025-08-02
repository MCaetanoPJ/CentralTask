
using CentralTask.Core.Mediator.Commands;
using CentralTask.Core.Notifications;
using CentralTask.Domain.Entidades;
using CentralTask.Domain.Interfaces.Repositories;

namespace CentralTask.Application.Commands.TasksCommands
{
    public class AlterarTasksCommandHandler : ICommandHandler<AlterarTasksCommandInput, AlterarTasksCommandResult>
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotifier _notifier;

        public AlterarTasksCommandHandler(ITasksRepository tasksRepository, INotifier notifier, IUserRepository userRepository)
        {
            _tasksRepository = tasksRepository;
            _notifier = notifier;
            _userRepository = userRepository;
        }

        public async Task<AlterarTasksCommandResult> Handle(AlterarTasksCommandInput request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Title))
            {
                _notifier.Notify("O título da tarefa é obrigatório.");
                return new();
            }

            if (string.IsNullOrEmpty(request.Description))
            {
                _notifier.Notify("A descrição da tarefa é obrigatório.");
                return new();
            }

            var taskBd = await _tasksRepository.GetByIdAsync(request.Id);
            if (taskBd == null)
            {
                _notifier.Notify("A tarefa não foi encontrada.");
                return new();
            }

            var user = _userRepository.GetAsNoTracking().FirstOrDefault(u => u.Id == request.UserId);
            if (user == null)
            {
                _notifier.Notify("O usuário informado não foi encontrado.");
                return new();
            }

            taskBd.Description = request.Description;
            taskBd.DueDate = request.DueDate;
            taskBd.Status = request.Status;
            taskBd.UserId = request.UserId;
            taskBd.Title = request.Title;

            _tasksRepository.Update(taskBd);

            await _tasksRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new AlterarTasksCommandResult { Id = taskBd.Id };
        }
    }
}