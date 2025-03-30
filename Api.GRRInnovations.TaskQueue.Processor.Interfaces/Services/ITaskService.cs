using Api.GRRInnovations.TaskQueue.Processor.Interfaces;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TaskQueue.Processor.Interfaces.Services
{
    public interface ITaskService
    {
        Task<ITaskModel?> GetByIdAsync(Guid id);
        Task<IEnumerable<ITaskModel>> GetAllAsync();
        Task<ITaskModel> CreateAsync(ITaskModel entity);
        Task<ITaskModel> UpdateAsync(ITaskModel entity);
        Task<bool> RetryAsync(Guid id);
        Task<bool> CancelAsync(Guid id);
    }
}
