using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.UserCommands.CriarUserCommand;

public class CriarUserCommandInput : CommandInput<CriarUserCommandResult>
{
    public string Nome { get; set; }
    public string Senha { get; set; }
    public string Email { get; set; }
}