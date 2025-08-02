using AutoMapper;
using CentralTask.Core.Extensions;
using CentralTask.Core.Mediator.Queries;
using CentralTask.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CentralTask.Application.Queries.UsuarioQueries.ObterUsuariosQueryPaginated;

public class ObterUsuariosQueryPaginatedHandler : IQueryHandler<ObterUsuariosQueryPaginatedInput, QueryPaginatedResult<ObterUsuariosQueryPaginatedItem>>
{
    private readonly IUsuarioRepository _repository;
    private readonly IMapper _mapper;

    public ObterUsuariosQueryPaginatedHandler(IUsuarioRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<QueryPaginatedResult<ObterUsuariosQueryPaginatedItem>> Handle(ObterUsuariosQueryPaginatedInput request, CancellationToken cancellationToken)
    {
        var (result, pagination) = await _repository
            .GetAsNoTracking()
            .Select(x => new ObterUsuariosQueryPaginatedItem
            {
                Id = x.Id,
                NomeCompleto = x.Nome,
                Email = x.Email,
                Status = (int)x.Status,
                NivelAcesso = (int)x.NivelAcesso
            })
            .PaginateAsync(request.PageNumber, request.PageSize, cancellationToken);

        return new()
        {
            Pagination = pagination,
            Result = result
        };
    }
}