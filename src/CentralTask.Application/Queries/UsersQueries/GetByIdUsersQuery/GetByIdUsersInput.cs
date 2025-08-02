
using CentralTask.Core.Mediator.Queries;

namespace CentralTask.Application.Queries.UsersQueries
{
    public class GetByIdUsersInput : QueryInput<QueryResult<GetByIdUsersItem>>
    {
        public Guid Id { get; set; }
    }
}