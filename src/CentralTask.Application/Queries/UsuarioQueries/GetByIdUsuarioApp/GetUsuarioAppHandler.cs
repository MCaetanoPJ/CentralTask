using CentralTask.Core.Mediator.Queries;
using CentralTask.Core.Notifications;
using CentralTask.Domain.Interfaces.Repositories;

namespace CentralTask.Application.Queries.UsuarioQueries.GetByIdUsuarioApp;

public class EnviarTokenRecuperacaoSenhaHandler
    : IQueryHandler<GetUsuarioAppInput,
    QueryResult<GetUsuarioAppItem>>
{

    private readonly INotifier _notifier;
    private readonly IUsuarioRepository _usuarioRepository;
    public EnviarTokenRecuperacaoSenhaHandler(
        INotifier notifier,
        IUsuarioRepository usuarioRepository)
    {
        _notifier = notifier;
        _usuarioRepository = usuarioRepository;
    }
    public async Task<QueryResult<GetUsuarioAppItem>>
        Handle(GetUsuarioAppInput request, CancellationToken cancellationToken)
    {
        var usuarioApp = _usuarioRepository.GetAsNoTracking()
            .Where(c => c.Id == request.UsuarioId).FirstOrDefault();

        if (usuarioApp == null)
        {
            return new QueryResult<GetUsuarioAppItem>();
        }

        return new GetUsuarioAppItem()
        {
            Celular = usuarioApp.Telefone,
            NomeCompelto = usuarioApp.NomeCompleto,
            DataNascimento = usuarioApp.DataNascimento,
        };
    }
}
