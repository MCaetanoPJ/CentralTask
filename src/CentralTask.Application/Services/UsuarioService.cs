using CentralTask.Domain.Interfaces.Repositories;
using CentralTask.Application.Services.Interfaces;
using CentralTask.Core.RepositoryBase;

namespace CentralTask.Application.Services;

public class UsuarioService : IUSuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(
        IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<ValidateResult> DeletarUsuario(Guid usuarioId)
    {
        var validacao = new ValidateResult();

        var usuario = _usuarioRepository
            .Get()
            .Where(c => c.Id == usuarioId)
            .FirstOrDefault();

        if (usuario == null)
        {
            validacao.AddMessage("Usuário informado não foi encontrado em nossa base de dados.");
            return validacao;
        }

        _usuarioRepository.Remove(usuario);
        await _usuarioRepository.UnitOfWork.SaveChangesAsync();

        return validacao;
    }
}