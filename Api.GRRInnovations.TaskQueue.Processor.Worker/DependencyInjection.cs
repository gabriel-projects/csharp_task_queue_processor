using Api.GRRInnovations.TaskQueue.Processor.Worker.Consumers;
using Microsoft.Extensions.DependencyInjection;

namespace Api.GRRInnovations.TaskQueue.Processor.Worker
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWorkerServices(this IServiceCollection services)
        {
            services.AddHostedService<TaskQueueConsumer>();

            return services;
        }
    }
}
