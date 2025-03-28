using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public static string ConnectionStringKey = "SqlConnectionString";

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

        internal static string ConnectionString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
            .AddJsonFile("local.settings.json", true)
#endif
            .AddEnvironmentVariables()
            .Build();

            var connection = configuration.GetConnectionString(ConnectionStringKey);

            Debug.WriteLine($"{nameof(ApplicationDbContextFactory)} sql connection string: {connection}");

            return connection;
        }
    }
}
