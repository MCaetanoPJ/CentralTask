
namespace CentralTask.Application.Queries.TasksQueries
{
    public class GetByIdTasksItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Duedate { get; set; }
        public Guid Userid { get; set; }
        public DateTime Createdat { get; set; }
    }
}