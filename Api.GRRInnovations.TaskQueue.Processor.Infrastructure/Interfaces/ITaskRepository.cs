using Api.GRRInnovations.TaskQueue.Processor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<ITaskModel>> GetAllAsync(TaskOptions taskOptions);
        Task<ITaskModel> GetByIdAsync(Guid uid);
        Task<ITaskModel> CreateAsync(ITaskModel task);
        Task<bool> RetryAsync(Guid id);
        Task<bool> CancelAsync(Guid id);
    }

    public class TaskOptions
    {
        public List<Guid> FilterUids { get; set; }
    }
}
