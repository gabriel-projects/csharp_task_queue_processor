using Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Persistence;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Testcontainers.MsSql;
using Testcontainers.RabbitMq;

namespace Api.GRRInnovations.TaskQueue.Processor.Tests.IntegrationTests.Helpers
{
    internal class ApiWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly IContainer _rabbitMqContainer;
        private readonly string _dbConnectionString;

        public ApiWebApplicationFactory(IContainer rabbitMqContainer, string dbConnectionString)
        {
            _rabbitMqContainer = rabbitMqContainer;
            _dbConnectionString = dbConnectionString;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(_dbConnectionString);
                });
            });

            builder.ConfigureAppConfiguration((context, config) =>
            {
                var rabbitHost = _rabbitMqContainer.Hostname;
                var rabbitPort = _rabbitMqContainer.GetMappedPublicPort(5672);

                config.AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["RabbitMqConnection:Host"] = rabbitHost,
                    ["RabbitMqConnection:Port"] = rabbitPort.ToString(),
                    ["RabbitMqConnection:Username"] = "guest",
                    ["RabbitMqConnection:Password"] = "guest",
                    ["ConnectionStrings:SqlConnectionString"] = _dbConnectionString
                });
            });
        }
    }
}
