
using CentralTask.Domain.Interfaces.Repositories;
using CentralTask.Core.Mediator.Commands;
using CentralTask.Core.Notifications;
using CentralTask.Domain.Entidades;
using CentralTask.Core.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace CentralTask.Application.Commands.UsersCommands
{
    public class AlterarUsersCommandHandler : ICommandHandler<AlterarUsersCommandInput, AlterarUsersCommandResult>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly INotifier _notifier;

        public AlterarUsersCommandHandler(IUsersRepository usersRepository, INotifier notifier)
        {
            _usersRepository = usersRepository;
            _notifier = notifier;
        }

        public async Task<AlterarUsersCommandResult> Handle(AlterarUsersCommandInput request, CancellationToken cancellationToken)
        {
            var entidade = new Users
            {
                Username = request.Username,
                Email = request.Email,
                Passwordhash = request.Passwordhash,
                Createdat = request.Createdat,

            };

              _usersRepository.Update(entidade);

            await _usersRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new AlterarUsersCommandResult { Id = entidade.Id };
        }
    }
}