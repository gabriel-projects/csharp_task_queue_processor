using Api.GRRInnovations.TaskQueue.Processor.Domain.Entities;
using Api.GRRInnovations.TaskQueue.Processor.Domain.Models;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Models;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Repositories;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TaskQueue.Processor.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<bool> CancelAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ITaskModel> CreateAsync(ITaskModel entity)
        {
            var mmodel = entity as TaskModel;
            if (mmodel == null) return null;



            await _taskRepository.CreateAsync(mmodel);

            return mmodel;
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

        public async Task<ITaskModel> UpdateAsync(ITaskModel entity)
        {
            var mmodel = entity as TaskModel;
            if (mmodel == null) return null;

            await _taskRepository.Update(mmodel);

            return mmodel;
        }

        public async Task<ITaskStatusSummary> GetStatusSummaryAsync()
        {
            return await _taskRepository.GetTaskStatusSummaryAsync();
        }
    }
}
