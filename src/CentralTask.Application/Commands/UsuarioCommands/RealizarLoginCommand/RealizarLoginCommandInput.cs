using CentralTask.Core.Mediator.Commands;
using CentralTask.Domain.Enums;

namespace CentralTask.Application.Commands.UsuarioCommands.RealizarLoginCommand;

public class RealizarLoginCommandInput : CommandInput<RealizarLoginCommandResult>
{
    public string Email { get; set; }
    public string Senha { get; set; }    
    public string DeviceId { get; set; }
}