
using CentralTask.Core.Extensions;
using CentralTask.Core.Mediator.Queries;
using CentralTask.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CentralTask.Application.Queries.UsersQueries
{
    public class GetPaginadoUsersHandler : IQueryHandler<GetPaginadoUsersInput, QueryPaginatedResult<GetPaginadoUsersItem>>
    {
        private readonly IUsersRepository _repository;

        public GetPaginadoUsersHandler(IUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<QueryPaginatedResult<GetPaginadoUsersItem>> Handle(GetPaginadoUsersInput request, CancellationToken cancellationToken)
        {
            var query = _repository.GetAsNoTracking();

            var (result, pagination) = await query
                .Select(x => new GetPaginadoUsersItem
                {
                    Username = x.Username,
                    Email = x.Email,
                    Passwordhash = x.Passwordhash,
                    Createdat = x.Createdat
                })
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            return new QueryPaginatedResult<GetPaginadoUsersItem>
            {
                Pagination = pagination,
                Result = result
            };
        }
    }
}