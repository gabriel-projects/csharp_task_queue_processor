using Api.GRRInnovations.TaskQueue.Processor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TaskQueue.Processor.Application.Interfaces
{
    public interface ITaskService
    {
        Task<ITaskModel?> GetByIdAsync(Guid id);
        Task<IEnumerable<ITaskModel>> GetAllAsync();
        Task<ITaskModel> CreateAsync(ITaskModel entity);
        Task UpdateAsync(ITaskModel entity);
        Task<bool> RetryAsync(Guid id);
        Task<bool> CancelAsync(Guid id);
    }
}
