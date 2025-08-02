using CentralTask.Domain.Entidades;
using CentralTask.Domain.Interfaces.Repositories;
using CentralTask.Infra.Data.Context;
using CentralTask.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CentralTask.Infra.Data.Repositories;
public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(CentralTaskContext context) : base(context)
    {
    }

    public Task<bool> UsuarioExiste(Guid id)
    {
        return GetAsNoTracking().AnyAsync(c => c.Id == id);
    }
    public async Task<List<Usuario>> GetAllAsync()
    {
        return GetAsNoTracking().ToList();
    }

    public Task<string> ObterFoto(Guid usuarioId)
    {
        return GetAsNoTracking()
            .Where(x => x.Id == usuarioId)
            .Select(x => x.Avatar)
            .FirstOrDefaultAsync();
    }
}
