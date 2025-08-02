using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Queries.UsuarioQueries.ObterUsuarioByIdQuery;

public class ObterUsuarioByIdQueryInput : CommandInput<ObterUsuarioByIdQueryItem>
{
    public Guid IdUsuario { get; set; }

}