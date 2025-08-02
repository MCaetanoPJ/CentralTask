using CentralTask.Core.Mediator.Commands;
using CentralTask.Domain.Enums;

namespace CentralTask.Application.Commands.UsuarioCommands.DeleteUsuarioCommand;

public class DeleteUsuarioCommandInput : CommandInput<DeleteUsuarioCommandResult>
{
    public Guid Id { get; set; }    
}