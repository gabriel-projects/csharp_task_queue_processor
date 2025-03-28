using Api.GRRInnovations.TaskQueue.Processor.Domain.Interfaces;
using Api.GRRInnovations.TaskQueue.Processor.Domain.Models;
using Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Persistence.Task;

namespace Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Persistence.Task
{
    public class TaskRepository : ITaskRepository
    {
        public Task<bool> CancelAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskModel>> GetAllAsync(TaskOptions options)
        {
            throw new NotImplementedException();
        }

        public Task<TaskModel?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TaskModel> Query(TaskOptions options)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RetryAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
