using Api.GRRInnovations.TaskQueue.Processor.Domain.Models;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.MessageBroker;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace Api.GRRInnovations.TaskQueue.Processor.Infrastructure.Publishers
{
    public class RabbitMQPublisher<T> : IRabbitMQPublisher<T>
    {
        private readonly RabbitMQSetting _rabbitMqSetting;
        private readonly AsyncRetryPolicy _retryPolicy;

        public RabbitMQPublisher(IOptions<RabbitMQSetting> rabbitMqSetting)
        {
            _rabbitMqSetting = rabbitMqSetting.Value;

            _retryPolicy = Policy
               .Handle<BrokerUnreachableException>()
               .Or<OperationInterruptedException>()
               .WaitAndRetryAsync(_rabbitMqSetting.RetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public async Task PublishMessageAsync(T message, string queueName)
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = _rabbitMqSetting.HostName,
                    UserName = _rabbitMqSetting.UserName,
                    Password = _rabbitMqSetting.Password
                };

                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(queue: queueName,
                   durable: true,
                   exclusive: false,
                   autoDelete: false,
                   arguments: new Dictionary<string, object>
                   {
                    { "x-dead-letter-exchange", "" },
                    { "x-dead-letter-routing-key", RabbitMQQueues.TaskQueueDead }
                   });

                var messageJson = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(messageJson);

                var props = new BasicProperties();

                await channel.BasicPublishAsync("", queueName, false, props, body);
            });
        }
    }
}
