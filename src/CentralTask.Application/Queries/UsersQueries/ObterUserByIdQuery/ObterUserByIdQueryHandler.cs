using AutoMapper;
using CentralTask.Core.Mediator.Commands;
using CentralTask.Domain.Interfaces.Repositories;

namespace CentralTask.Application.Queries.UserQueries.ObterUserByIdQuery;

public class ObterUserByIdQueryHandler : ICommandHandler<ObterUserByIdQueryInput, ObterUserByIdQueryItem>
{
    private readonly IUserRepository _UserRepository;
    private readonly IMapper _mapper;
    public ObterUserByIdQueryHandler(IUserRepository UserRepository,
        IMapper mapper)
    {
        _UserRepository = UserRepository;
        _mapper = mapper;
    }

    public async Task<ObterUserByIdQueryItem> Handle(ObterUserByIdQueryInput request, CancellationToken cancellationToken)
    {

        var result = _UserRepository.GetAsNoTracking().FirstOrDefault(x => x.Id == request.IdUser);
        if (result != null)
        {
            return new ObterUserByIdQueryItem()
            {
                UserId = result.Id,
                NomeCompleto = result.Nome,
                Email = result.Email,
                Status = (int)result.Active,
                NivelAcesso = (int)result.NivelAcesso,

            };
        }

        return new ObterUserByIdQueryItem();

    }
}

