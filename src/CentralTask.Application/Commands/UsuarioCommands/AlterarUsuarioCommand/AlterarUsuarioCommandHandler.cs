using AutoMapper;
using CentralTask.Core.Mediator.Commands;
using CentralTask.Core.Notifications;
using CentralTask.Domain.Entidades;
using CentralTask.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CentralTask.Domain.Enums;
using CentralTask.Core.Extensions;

namespace CentralTask.Application.Commands.UsuarioCommands.AlterarUsuarioCommand;

public class AlterarUsuarioCommandHandler : ICommandHandler<AlterarUsuarioCommandInput, AlterarUsuarioCommandResult>
{
    private readonly ILogger<AlterarUsuarioCommandHandler> _logger;
    private readonly UserManager<Usuario> _userManager;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly INotifier _notifier;
    private readonly IMapper _mapper;

    public AlterarUsuarioCommandHandler(
        ILogger<AlterarUsuarioCommandHandler> logger,
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

    public async Task<AlterarUsuarioCommandResult> Handle(AlterarUsuarioCommandInput request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository
            .Get()
            .Where(c => c.Id.Equals(request.IdUsuario))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (usuario is null)
        {
            _notifier.Notify("Usuario não encontrado.");
            return new();
        }

        if (request.Nome != null && request.Nome != string.Empty)
        {
            usuario.Nome = $"{request.Nome}" + " " + $"{request.SobreNome}";
        }

        if (request.NivelAcesso != usuario.NivelAcesso)
        {
            usuario.NivelAcesso = request.NivelAcesso;
        }

        if (request.Senha != null && request.Senha != string.Empty)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);

            var result = await _userManager.ResetPasswordAsync(usuario, token, request.Senha);
        }

        if (request.Email != null && request.Email != string.Empty)
        {
            usuario.Email = request.Email;
            usuario.NormalizedUserName = request.Email.ToUpper();
            usuario.NormalizedEmail = request.Email.ToUpper();
            usuario.UserName = request.Email.ToLower();
        }

        if (request.Cpf != null && request.Cpf != string.Empty)
        {
            usuario.Cpf = request.Cpf;
        }

        await _usuarioRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new AlterarUsuarioCommandResult() { Id = usuario.Id };
    }

}
