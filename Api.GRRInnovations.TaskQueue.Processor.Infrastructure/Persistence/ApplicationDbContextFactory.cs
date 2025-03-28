using Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var connection = ConnectionString();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(connection);

#if DEBUG
            builder.UseLoggerFactory(MyLoggerFactory);
            builder.EnableDetailedErrors(true);
            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            builder.EnableSensitiveDataLogging(true);
#endif

            return new ApplicationDbContext(builder.Options);
        }

        internal static string? ConnectionString()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
            .AddJsonFile($"appsettings.{environment}.json", true)
#endif
            .AddEnvironmentVariables()
            .Build();

            var connection = configuration.GetConnectionString(ConnectionHelper.ConnectionStringKey);

#if DEBUG
            Console.WriteLine($"[{environment}] {nameof(ApplicationDbContextFactory)} SQL Connection String: {connection}");
#endif

            return connection;
        }
    }
}
