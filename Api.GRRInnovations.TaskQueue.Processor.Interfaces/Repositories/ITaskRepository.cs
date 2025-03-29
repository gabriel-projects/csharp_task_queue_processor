using Api.GRRInnovations.TaskQueue.Processor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TaskQueue.Processor.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<bool> RetryAsync(Guid id);
        Task<bool> CancelAsync(Guid id);
    }

    public class TaskOptions
    {
        public List<Guid>? FilterUids { get; set; }
    }
}
