using Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Persistence;
using DotNet.Testcontainers.Builders;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Respawn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;
using Testcontainers.RabbitMq;

namespace Api.GRRInnovations.TaskQueue.Processor.Tests.IntegrationTests.Containers
{
    public class IntegrationTestFixture : IAsyncLifetime
    {
        public RabbitMqContainer RabbitMqContainer { get; private set; }
        public MsSqlContainer SqlContainer { get; private set; }

        public async Task InitializeAsync()
        {
            RabbitMqContainer = new RabbitMqBuilder()
                .WithImage("rabbitmq:3-management")
                .WithPortBinding(5672, true)
                .WithPortBinding(15672, true)
                .WithEnvironment("RABBITMQ_DEFAULT_USER", "guest")
                .WithEnvironment("RABBITMQ_DEFAULT_PASS", "guest")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5672))
                .Build();


            SqlContainer = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
                .WithPassword("Password123")
                .WithName(Guid.NewGuid().ToString())
                .WithCleanUp(true)
                .Build();

            await RabbitMqContainer.StartAsync();
            await SqlContainer.StartAsync();

            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(SqlContainer.GetConnectionString())
                .Options;

            using var dbContext = new ApplicationDbContext(dbContextOptions);
            dbContext.Database.EnsureCreated();
        }

        public async Task DisposeAsync()
        {
            await RabbitMqContainer.StopAsync();
            await SqlContainer.StopAsync();
        }

        public async Task ClearDatabaseAsync()
        {
            using var connection = new SqlConnection(SqlContainer.GetConnectionString());
            await connection.OpenAsync();
            var respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.SqlServer
            });

            await respawner.ResetAsync(connection);
        }
    }
}
