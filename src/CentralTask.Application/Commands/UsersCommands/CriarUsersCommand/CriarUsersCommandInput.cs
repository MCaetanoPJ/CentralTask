
using CentralTask.Core.Mediator.Commands;

namespace CentralTask.Application.Commands.UsersCommands
{
    public class CriarUsersCommandInput : CommandInput<CriarUsersCommandResult>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Passwordhash { get; set; }
        public DateTime Createdat { get; set; }
    }
}
