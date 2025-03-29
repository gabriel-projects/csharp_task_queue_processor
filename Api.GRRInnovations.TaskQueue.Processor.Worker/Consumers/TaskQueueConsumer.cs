using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Api.GRRInnovations.TaskQueue.Processor.Worker.Consumers
{
    public class TaskQueueConsumer : BackgroundService
    {
        private readonly ILogger<TaskQueueConsumer> _logger;
        private readonly ConnectionFactory _factory;
        private IChannel _channel;
        private IConnection _connection;
        private readonly string _queueName = "minha-fila";

        public TaskQueueConsumer(ILogger<TaskQueueConsumer> logger)
        {

            _logger = logger;
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "admin123",
                VirtualHost = "/"
            };

            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Inicializando conexão com RabbitMQ...");

            _connection = await _factory.CreateConnectionAsync(stoppingToken);
            _channel = await _connection.CreateChannelAsync();

            await _channel.QueueDeclareAsync(queue: _queueName,
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _logger.LogInformation("Mensagem recebida: {message}", message);

                await Task.Delay(1000);

                await _channel.BasicAckAsync(ea.DeliveryTag, false);
            };

            string consumerTag = await _channel.BasicConsumeAsync(_queueName, false, consumer);

            _logger.LogInformation("RabbitMQ Consumer iniciado. Aguardando mensagens...");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000, stoppingToken);
            }
        }

        public override void Dispose()
        {
            _channel?.CloseAsync();
            _connection?.CloseAsync();
            base.Dispose();
        }
    }
}
