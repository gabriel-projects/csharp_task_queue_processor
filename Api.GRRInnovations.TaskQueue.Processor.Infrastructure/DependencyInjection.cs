using Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Helpers;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Services;
using Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Persistence;
using Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Publishers;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Persistence.Repositories;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.MessageBroker;

namespace Api.GRRInnovations.TaskQueue.Processor.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped(typeof(IRabbitMQPublisher<>), typeof(RabbitMQPublisher<>));

            var connection = ConnectionHelper.GetConnectionString(configuration);

            services.AddDbContextPool<ApplicationDbContext>(options => ConfigureDatabase(options, connection));


            return services;
        }

        private static void ConfigureDatabase(DbContextOptionsBuilder options, string connection)
        {
            options.UseSqlServer(connection, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null
                );

                sqlOptions.CommandTimeout(60);
            });

#if DEBUG
            options.LogTo(Console.WriteLine, LogLevel.Information)
                   .EnableSensitiveDataLogging(); // CUIDADO: isso mostra dados sensíveis no log
#endif
        }
    }
}
