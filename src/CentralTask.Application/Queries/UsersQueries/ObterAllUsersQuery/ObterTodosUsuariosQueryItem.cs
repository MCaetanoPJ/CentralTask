namespace CentralTask.Application.Queries.UserQueries.ObterTodosUserQuery;

public class ObterTodosUsersQueryItem
{
    public Guid Id { get; set; }
    public string NomeCompleto { get; set; }
    public string Email { get; set; }
    public int Status { get; set; }
    public string NivelAcesso { get; set; }
}

