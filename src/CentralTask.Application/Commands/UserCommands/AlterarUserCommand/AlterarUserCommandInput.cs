using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.UserCommands.AlterarUserCommand;

public class AlterarUserCommandInput : CommandInput<AlterarUserCommandResult>
{
    public Guid IdUser { get; set; }
    public string Email { get; set; }
    public string Nome { get; set; }
    public string Senha { get; set; }
}