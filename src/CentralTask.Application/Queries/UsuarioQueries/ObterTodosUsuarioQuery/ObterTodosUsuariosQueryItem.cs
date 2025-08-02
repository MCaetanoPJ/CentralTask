using CentralTask.Domain.Enums;

namespace CentralTask.Application.Queries.UsuarioQueries.ObterTodosUsuarioQuery;

public class ObterTodosUsuariosQueryItem
{
    public Guid Id { get; set; }
    public string NomeCompleto { get; set; }
    public string Email { get; set; }
    public int Status { get; set; }
    public string NivelAcesso { get; set; }
}

