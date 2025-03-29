using Api.GRRInnovations.TaskQueue.Processor.Application.Interfaces;
using Api.GRRInnovations.TaskQueue.Processor.Application.Services;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.GRRInnovations.TaskQueue.Processor.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();

            return services;
        }
    }
}
