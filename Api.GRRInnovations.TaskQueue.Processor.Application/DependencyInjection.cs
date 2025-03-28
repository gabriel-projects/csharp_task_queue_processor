using Microsoft.Extensions.DependencyInjection;

namespace Api.GRRInnovations.TaskQueue.Processor.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
