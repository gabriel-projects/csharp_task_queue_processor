using Api.GRRInnovations.TaskQueue.Processor.Domain.Interfaces;
using Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Interfaces;

namespace Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public Task<bool> CancelAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ITaskModel> CreateAsync(ITaskModel task)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ITaskModel>> GetAllAsync(TaskOptions taskOptions)
        {
            throw new NotImplementedException();
        }

        public Task<ITaskModel> GetByIdAsync(Guid uid)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RetryAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
