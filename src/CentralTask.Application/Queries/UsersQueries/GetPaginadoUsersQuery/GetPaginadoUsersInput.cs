
using CentralTask.Core.Mediator.Queries;

namespace CentralTask.Application.Queries.UsersQueries
{
    public class GetPaginadoUsersInput : QueryPaginatedInput<QueryPaginatedResult<GetPaginadoUsersItem>>
    {
        public string? Nome { get; set; }
    }
}