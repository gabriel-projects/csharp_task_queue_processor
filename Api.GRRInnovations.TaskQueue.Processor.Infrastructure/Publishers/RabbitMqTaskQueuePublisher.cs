using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Models;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Services;
using RabbitMQ.Client;
using System.Text;

namespace Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Publishers
{
    public class RabbitMqTaskQueuePublisher : ITaskQueuePublisher
    {
        private readonly ConnectionFactory _factory;
        private readonly string _queueName = "minha-fila";

        public RabbitMqTaskQueuePublisher()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "admin123",
                VirtualHost = "/"
            };
        }

        public async Task PublishAsync(ITaskModel task)
        {
            var connection = await _factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: _queueName,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

            var json = System.Text.Json.JsonSerializer.Serialize(task);

            var body = Encoding.UTF8.GetBytes(json);
            var props = new BasicProperties();

            await channel.BasicPublishAsync("", _queueName, false, props, body);
        }
    }
}
