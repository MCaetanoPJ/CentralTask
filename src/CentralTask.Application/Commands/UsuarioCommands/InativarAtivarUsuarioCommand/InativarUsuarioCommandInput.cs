using CentralTask.Core.Mediator.Commands;
using CentralTask.Domain.Enums;

namespace CentralTask.Application.Commands.UsuarioCommands.InativarAtivarUsuarioCommand;

public class InativarUsuarioCommandInput : CommandInput<InativarUsuarioCommandResult>
{
    public Guid Id { get; set; }    
}