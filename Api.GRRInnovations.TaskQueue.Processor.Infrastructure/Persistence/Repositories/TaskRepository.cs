using Api.GRRInnovations.TaskQueue.Processor.Domain.Entities;
using Api.GRRInnovations.TaskQueue.Processor.Domain.Models;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Models;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<bool> CancelAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task is null) return false;

            task.Cancel();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ITaskModel> CreateAsync(ITaskModel entity)
        {
            var mmodel = entity as TaskModel;
            if (mmodel == null) return null;

            await _context.Tasks.AddAsync(mmodel).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return mmodel;
        }

        public Task<IEnumerable<ITaskModel>> GetAllAsync(TaskOptions options)
        {
            throw new NotImplementedException();
        }

        public async Task<ITaskModel?> GetByIdAsync(Guid id)
        {
            var task = await _context.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);

            return task;
        }

        public async Task<ITaskStatusSummary> GetTaskStatusSummaryAsync()
        {
            var summary = await _context.Tasks
                .GroupBy(e => e.Status)
                .Select(e => new
                {
                    Status = e.Key,
                    Count = e.Count()
                })
                .ToListAsync();

            var result = new TaskStatusSummary();

            foreach (var item in summary)
            {
                switch (item.Status)
                {
                    case Interfaces.Enums.ETaskStatus.Pending:
                        result.Pending = item.Count;
                        break;
                    case Interfaces.Enums.ETaskStatus.Processing:
                        result.Processing = item.Count;
                        break;
                    case Interfaces.Enums.ETaskStatus.Completed:
                        result.Completed = item.Count;
                        break;
                    case Interfaces.Enums.ETaskStatus.Error:
                        result.Error = item.Count;
                        break;
                    case Interfaces.Enums.ETaskStatus.Cancelled:
                        result.Cancelled = item.Count;
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        public async Task<bool> RetryAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task is null) return false;

            task.Retry();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ITaskModel> Update(ITaskModel entity)
        {
            var mmodel = entity as TaskModel;
            if (mmodel == null) return null;

            _context.Update(mmodel);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return mmodel;
        }
    }
}
