using AutoMapper;
using CentralTask.Core.Extensions;
using CentralTask.Core.Mediator.Queries;
using CentralTask.Domain.Interfaces.Repositories;

namespace CentralTask.Application.Queries.UserQueries.ObterUsersQueryPaginated;

public class ObterUsersQueryPaginatedHandler : IQueryHandler<ObterUsersQueryPaginatedInput, QueryPaginatedResult<ObterUsersQueryPaginatedItem>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public ObterUsersQueryPaginatedHandler(IUserRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<QueryPaginatedResult<ObterUsersQueryPaginatedItem>> Handle(ObterUsersQueryPaginatedInput request, CancellationToken cancellationToken)
    {
        var (result, pagination) = await _repository
            .GetAsNoTracking()
            .Select(x => new ObterUsersQueryPaginatedItem
            {
                Id = x.Id,
                NomeCompleto = x.Nome,
                Email = x.Email,
                Status = (int)x.Active
            })
            .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

        return new()
        {
            Pagination = pagination,
            Result = result
        };
    }
}