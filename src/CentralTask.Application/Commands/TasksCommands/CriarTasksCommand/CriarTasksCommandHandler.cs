
using CentralTask.Core.Mediator.Commands;
using CentralTask.Core.Notifications;
using CentralTask.Domain.Entidades;
using CentralTask.Domain.Interfaces.Repositories;
using ChoETL;

namespace CentralTask.Application.Commands.TasksCommands
{
    public class CriarTasksCommandHandler : ICommandHandler<CriarTasksCommandInput, CriarTasksCommandResult>
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotifier _notifier;

        public CriarTasksCommandHandler(ITasksRepository tasksRepository, INotifier notifier, IUserRepository userRepository)
        {
            _tasksRepository = tasksRepository;
            _notifier = notifier;
            _userRepository = userRepository;
        }

        public async Task<CriarTasksCommandResult> Handle(CriarTasksCommandInput request, CancellationToken cancellationToken)
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

            var user = _userRepository.GetAsNoTracking().FirstOrDefault(u => u.Id == request.UserId);
            if (user == null)
            {
                _notifier.Notify("O usuário informado não foi encontrado.");
                return new();
            }

            var entidade = new Tasks
            {
                Status = request.Status,
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                UserId = request.UserId
            };

            _tasksRepository.Add(entidade);

            await _tasksRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new CriarTasksCommandResult { Id = entidade.Id };
        }
    }
}