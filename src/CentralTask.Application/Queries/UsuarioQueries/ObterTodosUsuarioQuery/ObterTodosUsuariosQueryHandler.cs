using AutoMapper;
using CentralTask.Core.Mediator.Queries;
using CentralTask.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CentralTask.Application.Queries.UsuarioQueries.ObterTodosUsuarioQuery;

public class ObterTodosUsuariosQueryHandler
    : IQueryHandler<GetAllUsuarioQueryInput,
    QueryListResult<ObterTodosUsuariosQueryItem>>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;
    public ObterTodosUsuariosQueryHandler(
        IUsuarioRepository usuarioRepository,
        IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }
    public Task<QueryListResult<ObterTodosUsuariosQueryItem>>
        Handle(GetAllUsuarioQueryInput request, CancellationToken cancellationToken)
    {
        var usuarios = _mapper.Map<List<ObterTodosUsuariosQueryItem>>
            (_usuarioRepository.GetAsNoTracking().ToList());

        return Task.FromResult(new QueryListResult<ObterTodosUsuariosQueryItem>
        {
            Result = usuarios
        });
    }
}
