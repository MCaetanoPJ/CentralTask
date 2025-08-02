
using CentralTask.Core.Extensions;
using CentralTask.Core.Mediator.Queries;
using CentralTask.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CentralTask.Application.Queries.TasksQueries
{
    public class GetPaginadoTasksHandler : IQueryHandler<GetPaginadoTasksInput, QueryPaginatedResult<GetPaginadoTasksItem>>
    {
        private readonly ITasksRepository _repository;

        public GetPaginadoTasksHandler(ITasksRepository repository)
        {
            _repository = repository;
        }

        public async Task<QueryPaginatedResult<GetPaginadoTasksItem>> Handle(GetPaginadoTasksInput request, CancellationToken cancellationToken)
        {
            var query = _repository.GetAsNoTracking();

            var (result, pagination) = await query
                .Select(x => new GetPaginadoTasksItem
                {
                    Title = x.Title,
                    Description = x.Description,
                    Duedate = x.Duedate,
                    Userid = x.Userid,
                    Createdat = x.Createdat
                })
                .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

            return new QueryPaginatedResult<GetPaginadoTasksItem>
            {
                Pagination = pagination,
                Result = result
            };
        }
    }
}