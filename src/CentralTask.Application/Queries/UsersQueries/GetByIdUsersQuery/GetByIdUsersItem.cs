
namespace CentralTask.Application.Queries.UsersQueries
{
    public class GetByIdUsersItem
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Passwordhash { get; set; }
        public DateTime Createdat { get; set; }
    }
}