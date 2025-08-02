using CentralTask.Domain.Entidades;
using CentralTask.Domain.Interfaces.Repositories.Base;

namespace CentralTask.Domain.Interfaces.Repositories;

public interface IUsuarioRepository : IGenericRepository<Usuario>
{
    public Task<bool> UsuarioExiste(Guid id);
    Task<List<Usuario>> GetAllAsync();
    Task<string> ObterFoto(Guid usuarioId);
}
