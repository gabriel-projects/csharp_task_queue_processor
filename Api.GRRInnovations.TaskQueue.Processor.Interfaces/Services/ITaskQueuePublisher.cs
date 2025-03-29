using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Models;

namespace Api.GRRInnovations.TaskQueue.Processor.Interfaces.Services
{
    public interface ITaskQueuePublisher
    {
        Task PublishAsync(ITaskModel task);
    }
}
