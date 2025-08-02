
using CentralTask.Domain.Interfaces.Repositories;
using CentralTask.Core.Mediator.Commands;
using CentralTask.Core.Notifications;
using CentralTask.Domain.Entidades;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CentralTask.Application.Commands.UsersCommands
{
    public class DeletarUsersCommandHandler : ICommandHandler<DeletarUsersCommandInput, DeletarUsersCommandResult>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly INotifier _notifier;

        public DeletarUsersCommandHandler(IUsersRepository usersRepository, INotifier notifier)
        {
            _usersRepository = usersRepository;
            _notifier = notifier;
        }

        public async Task<DeletarUsersCommandResult> Handle(DeletarUsersCommandInput request, CancellationToken cancellationToken)
        {
            var entidade = _usersRepository.Get().FirstOrDefault(c => c.Id == request.Id);
            
            _usersRepository.Remove(entidade);
            await _usersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new DeletarUsersCommandResult();
        }
    }
}
