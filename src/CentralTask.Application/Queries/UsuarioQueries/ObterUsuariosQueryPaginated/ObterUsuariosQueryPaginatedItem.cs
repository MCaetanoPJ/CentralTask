namespace CentralTask.Application.Queries.UsuarioQueries.ObterUsuariosQueryPaginated;

public class ObterUsuariosQueryPaginatedItem
{
    public Guid Id { get; set; }
    public int NivelAcesso { get; set; }
    public string NomeCompleto { get; set; }
    public string Email { get; set; }
    public int Status { get; set; }
}