using Api.GRRInnovations.TaskQueue.Processor.Domain.Entities;
using Api.GRRInnovations.TaskQueue.Processor.Domain.Wrappers.In;
using Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Persistence;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Enums;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Models;
using Api.GRRInnovations.TaskQueue.Processor.Tests.IntegrationTests.Containers;
using Api.GRRInnovations.TaskQueue.Processor.Tests.IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TaskQueue.Processor.Tests.IntegrationTests.QueueProcessing
{
    public class QueueProcessingTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly IntegrationTestFixture _fixture;

        public QueueProcessingTests(IntegrationTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Deve_Conectar_Banco_De_Teste()
        {
            var connectionString = _fixture.SqlContainer.GetConnectionString();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var context = new ApplicationDbContext(options);

            // Aplica migrations, se necessário
            context.Database.Migrate();

            // Testa alguma operação
            context.Tasks.Add(new TaskModel { Description = "Teste" });
            await context.SaveChangesAsync();

            var tarefas = await context.Tasks.ToListAsync();
            Assert.NotEmpty(tarefas);
        }

        [Fact]
        public async Task Tarefa_Enviada_Deve_Ser_Consumida_E_Salva()
        {
            // Arrange
            var factory = new ApiWebApplicationFactory(_fixture.RabbitMqContainer, _fixture.SqlContainer.GetConnectionString());
            var client = factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("https://localhost:7013")
            });

            await Task.Delay(5000);

            var taskModel = new WrapperInTask<TaskModel>
            {
                Type = ETaskType.Email.ToString(),
                Priority = ETaskPriority.High.ToString(),
                Description = "Teste de envio de e-mail",
            };

            // Act
            var response = await client.PostAsJsonAsync("api/tasks", taskModel);
            response.EnsureSuccessStatusCode();

            // Aguarda processamento assíncrono (simples, mas funciona em testes)
            await Task.Delay(5000);

            // Assert
            // Aqui você consulta o banco via EF Core
            var connectionString = _fixture.SqlContainer.GetConnectionString();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var context = new ApplicationDbContext(options);
            var tarefa = await context.Tasks.FirstOrDefaultAsync(t => t.Description == "Teste de envio de e-mail");
            Assert.NotNull(tarefa);
        }
    }
}
