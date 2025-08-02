using CentralTask.Core.RepositoryBase;

namespace CentralTask.Application.Services.Interfaces;

public interface IUSuarioService
{
    Task<ValidateResult> DeletarUsuario(Guid usuarioId);
}
