﻿using Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Helpers
{
    public static class ConnectionHelper
    {
        public const string ConnectionStringKey = "SqlConnectionString";

        internal static string? GetConnectionString(IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString(ConnectionStringKey);

            Debug.WriteLine($"{nameof(ApplicationDbContextFactory)} sql connection string: {connection}");

            return connection;
        }
    }
}
