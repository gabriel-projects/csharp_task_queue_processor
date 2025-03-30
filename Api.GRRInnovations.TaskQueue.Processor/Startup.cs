using Api.GRRInnovations.TaskQueue.Processor.Application;
using Api.GRRInnovations.TaskQueue.Processor.Domain.Models;
using Api.GRRInnovations.TaskQueue.Processor.Infrastructure;
using Api.GRRInnovations.TaskQueue.Processor.Worker;
using Microsoft.Extensions.DependencyInjection;
using static System.Collections.Specialized.BitVector32;

namespace Api.GRRInnovations.TaskQueue.Processor
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var rabbitMqSection = _configuration.GetSection("RabbitMqConnection");
            if (!rabbitMqSection.Exists())
            {
                throw new InvalidOperationException("Seção 'RabbitMqConnection' não encontrada no appsettings.json.");
            }

            services.AddOptions<RabbitMQSetting>()
                .BindConfiguration("RabbitMqConnection")
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddInfrastructureServices(_configuration)
                .AddApplicationServices()
                .AddWorkerServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
