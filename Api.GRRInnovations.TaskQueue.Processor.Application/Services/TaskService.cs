using Api.GRRInnovations.TaskQueue.Processor.Application.Interfaces;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Models;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TaskQueue.Processor.Application.Services
{
    public class TaskService : ITaskService
    {
        public Task<bool> CancelAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ITaskModel> CreateAsync(ITaskModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ITaskModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ITaskModel?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RetryAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ITaskModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
