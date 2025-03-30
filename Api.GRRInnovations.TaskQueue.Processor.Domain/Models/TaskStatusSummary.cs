using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Models;

namespace Api.GRRInnovations.TaskQueue.Processor.Domain.Models
{
    public class TaskStatusSummary : ITaskStatusSummary
    {
        public int Pending { get; set; }
        public int Processing { get; set; }
        public int Completed { get; set; }
        public int Cancelled { get; set; }
        public int Error { get; set; }
    }
}
