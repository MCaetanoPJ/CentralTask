using CentralTask.Core.Mediator.Commands;
using CentralTask.Domain.Enums;

namespace CentralTask.Application.Commands.UsuarioCommands.AlterarUsuarioCommand;

public class AlterarUsuarioCommandInput : CommandInput<AlterarUsuarioCommandResult>
{
    public Guid IdUsuario { get; set; }
    public string Email { get; set; }
    public string Nome { get; set; }
    public string SobreNome { get; set; }
    public string Senha { get; set; }
    public string Cpf { get; set; }
    public bool ReceberEmail { get; set; }
    public EnumNivel NivelAcesso { get; set; }
}