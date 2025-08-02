using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Queries.UsuarioQueries.ObterUsuarioByIdQuery;

public class ObterUsuarioByIdQueryItem : CommandResult
{
    public Guid UsuarioId { get; set; }
    public string NomeCompleto { get; set; }
    public string Email { get; set; }
    public string Sexo { get; set; }
    public string Telefone { get; set; }
    public int Status { get; set; }
    public int NivelAcesso { get; set; }
    public string Avatar { get; set; }
    public DateTime DataNascimento { get; set; }
    public int Idade { get; set; }
    public DateTime DataCadastro { get; set; }
    public Guid DenominacaoId { get; set; }
}