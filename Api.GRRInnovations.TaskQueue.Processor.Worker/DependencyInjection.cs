using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TaskQueue.Processor.Worker
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWorkerServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
