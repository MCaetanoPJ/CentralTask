
using CentralTask.Infra.Data.Context;
using CentralTask.Infra.Data.Repositories.Base;
using CentralTask.Domain.Entidades;
using CentralTask.Domain.Interfaces.Repositories;

namespace CentralTask.Infra.Data.Repositories
{
    public class TasksRepository : GenericRepository<Tasks>, ITasksRepository
    {
        public TasksRepository(CentralTaskContext context) : base(context)
        {
        }
    }
}