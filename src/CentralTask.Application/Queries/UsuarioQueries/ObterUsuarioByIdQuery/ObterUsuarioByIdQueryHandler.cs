using AutoMapper;
using CentralTask.Core.Mediator.Commands;
using CentralTask.Domain.Interfaces.Repositories;

namespace CentralTask.Application.Queries.UsuarioQueries.ObterUsuarioByIdQuery;

public class ObterUsuarioByIdQueryHandler : ICommandHandler<ObterUsuarioByIdQueryInput, ObterUsuarioByIdQueryItem>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;
    public ObterUsuarioByIdQueryHandler(IUsuarioRepository usuarioRepository,
        IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    public async Task<ObterUsuarioByIdQueryItem> Handle(ObterUsuarioByIdQueryInput request, CancellationToken cancellationToken)
    {

        var result = _usuarioRepository.GetAsNoTracking().FirstOrDefault(x => x.Id == request.IdUsuario);
        if (result != null)
        {
            return new ObterUsuarioByIdQueryItem()
            {
                UsuarioId = result.Id,
                NomeCompleto = result.Nome,
                Email = result.Email,
                Status = (int)result.Status,
                NivelAcesso =(int)result.NivelAcesso,
                
            };
        }

        return new ObterUsuarioByIdQueryItem();

    }
}

