using CentralTask.Core.Mediator.Queries;

namespace CentralTask.Application.Queries.UsuarioQueries.GetByIdUsuarioApp;

public class GetUsuarioAppInput
    : QueryInput<QueryResult<GetUsuarioAppItem>>
{
    public Guid UsuarioId { get; set; }
}
