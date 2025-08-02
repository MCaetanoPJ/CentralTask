using CentralTask.Core.Mediator.Commands;
using CentralTask.Domain.Enums;

namespace CentralTask.Application.Commands.UsuarioCommands.CriarUsuarioCommand;

public class CriarUsuarioCommandInput : CommandInput<CriarUsuarioCommandResult>
{
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Cpf { get; set; }
    public string Senha { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Avatar { get; set; }
    public DateTime DataNascimento { get; set; }
    public EnumNivel NivelAcesso { get; set; }
}