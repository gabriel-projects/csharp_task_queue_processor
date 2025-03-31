using Api.GRRInnovations.TaskQueue.Processor.Tests.IntegrationTests.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TaskQueue.Processor.Tests.IntegrationTests.Helpers
{
    [CollectionDefinition(Name)]
    public class DatabaseCollection : ICollectionFixture<IntegrationTestFixture> 
    {
        public const string Name = nameof(DatabaseCollection);
    }
}
