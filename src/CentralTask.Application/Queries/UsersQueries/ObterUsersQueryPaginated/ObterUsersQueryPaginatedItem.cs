namespace CentralTask.Application.Queries.UserQueries.ObterUsersQueryPaginated;

public class ObterUsersQueryPaginatedItem
{
    public Guid Id { get; set; }
    public int NivelAcesso { get; set; }
    public string NomeCompleto { get; set; }
    public string Email { get; set; }
    public int Status { get; set; }
}