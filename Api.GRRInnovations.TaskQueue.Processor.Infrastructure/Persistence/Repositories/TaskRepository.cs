using Api.GRRInnovations.TaskQueue.Processor.Domain.Models;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Repositories;

namespace Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Persistence.Repositories
{
    public class TaskRepository : Repository<TaskModel, TaskOptions>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> CancelAsync(Guid id)
        {
            var task = await _dbSet.FindAsync(id);
            if (task is null) return false;

            task.Cancel();

            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> RetryAsync(Guid id)
        {
            var task = await _dbSet.FindAsync(id);
            if (task is null) return false;

            task.Retry();
            await SaveChangesAsync();
            return true;
        }

        protected override IQueryable<TaskModel> ApplyFiltering(IQueryable<TaskModel> query, TaskOptions options)
        {
            if (options.FilterUids != null)
            {
                query = query = query.Where(p => options.FilterUids.Contains(p.Uid));
            }

            return query;
        }
    }
}
