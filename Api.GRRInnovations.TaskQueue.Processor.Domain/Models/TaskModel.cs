using Api.GRRInnovations.TaskQueue.Processor.Domain.Enums;
using Api.GRRInnovations.TaskQueue.Processor.Domain.Interfaces;

namespace Api.GRRInnovations.TaskQueue.Processor.Domain.Models
{
    public class TaskModel : BaseModel, ITaskModel
    {
        public string Description { get; set; } = string.Empty;
        public ETaskType Type { get; set; }
        public ETaskPriority Priority { get; set; }
        public ETaskStatus Status { get; set; } = ETaskStatus.Pending;
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
