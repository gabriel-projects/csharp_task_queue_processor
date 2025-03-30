using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Models;

namespace Api.GRRInnovations.TaskQueue.Processor.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<ITaskModel> CreateAsync(ITaskModel entity);
        void Update(ITaskModel entity);
        Task<ITaskModel?> GetByIdAsync(Guid id);
        Task<IEnumerable<ITaskModel>> GetAllAsync(TaskOptions options);
        Task<bool> RetryAsync(Guid id);
        Task<bool> CancelAsync(Guid id);
    }

    public class TaskOptions
    {
        public List<Guid>? FilterUids { get; set; }
    }
}
