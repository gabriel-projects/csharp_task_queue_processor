using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TaskQueue.Processor.Interfaces.Models
{
    public interface ITaskModel : IBaseModel
    {
        string Description { get; set; }
        ETaskType Type { get; set; }
        ETaskPriority Priority { get; set; }
        ETaskStatus Status { get; set; }
        DateTime? StartedAt { get; set; }
        DateTime? CompletedAt { get; set; }
        string? ErrorMessage { get; set; }
    }
}
