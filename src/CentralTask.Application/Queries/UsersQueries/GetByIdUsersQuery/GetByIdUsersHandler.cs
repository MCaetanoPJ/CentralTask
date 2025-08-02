
using CentralTask.Core.Mediator.Queries;
using CentralTask.Domain.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CentralTask.Application.Queries.UsersQueries
{
    public class GetByIdUsersHandler : IQueryHandler<GetByIdUsersInput, QueryResult<GetByIdUsersItem>>
    {
        private readonly IUsersRepository _repository;

        public GetByIdUsersHandler(IUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<QueryResult<GetByIdUsersItem>> Handle(GetByIdUsersInput request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (result == null)
                return null;

            return new QueryResult<GetByIdUsersItem>
            {
                Result = new GetByIdUsersItem
                {
                    Username = result.Username,
                    Email = result.Email,
                    Passwordhash = result.Passwordhash,
                    Createdat = result.Createdat
                }
            };
        }
    }
}