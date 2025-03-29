using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Models;

namespace Api.GRRInnovations.TaskQueue.Processor.Domain.Models
{
    public class BaseModel : IBaseModel
    {
        public Guid Uid { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
