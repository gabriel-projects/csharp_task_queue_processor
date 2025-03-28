using Api.GRRInnovations.TaskQueue.Processor.Application;
using Api.GRRInnovations.TaskQueue.Processor.Infrastructure;
using Api.GRRInnovations.TaskQueue.Processor.Worker;

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

            services.AddInfrastructureServices(_configuration);
            services.AddApplicationServices();
            services.AddWorkerServices();
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
