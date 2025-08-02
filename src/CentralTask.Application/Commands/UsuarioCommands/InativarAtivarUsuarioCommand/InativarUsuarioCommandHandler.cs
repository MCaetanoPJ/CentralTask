using AutoMapper;
using CentralTask.Core.Mediator.Commands;
using CentralTask.Core.Notifications;
using CentralTask.Domain.Entidades;
using CentralTask.Domain.Enums;
using CentralTask.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CentralTask.Application.Commands.UsuarioCommands.InativarAtivarUsuarioCommand;

public class InativarUsuarioCommandHandler : ICommandHandler<InativarUsuarioCommandInput, InativarUsuarioCommandResult>
{
    private readonly ILogger<InativarUsuarioCommandHandler> _logger;
    private readonly UserManager<Usuario> _userManager;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly INotifier _notifier;
    private readonly IMapper _mapper;

    public InativarUsuarioCommandHandler(
        ILogger<InativarUsuarioCommandHandler> logger,
        UserManager<Usuario> userManager,
        IUsuarioRepository usuarioRepository,
        IMapper mapper,
        INotifier notifier)
    {
        _logger = logger;
        _userManager = userManager;
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
        _notifier = notifier;
    }

    public async Task<InativarUsuarioCommandResult> Handle(InativarUsuarioCommandInput request, CancellationToken cancellationToken)
    {
        var inativarUsuario = _usuarioRepository.Get().FirstOrDefault(x => x.Id == request.Id);

        if (inativarUsuario != null)
        {
            if (inativarUsuario.Status == Status.Ativo)
                inativarUsuario.Status = Status.Inativo;
            else
                inativarUsuario.Status = Status.Ativo;

            _usuarioRepository.Update(inativarUsuario);

            await _usuarioRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new InativarUsuarioCommandResult { Id = inativarUsuario.Id };
        }

        _notifier.Notify($"Não foi localizado Usuário no nosso sistema.");
        return new InativarUsuarioCommandResult();
    }
}
