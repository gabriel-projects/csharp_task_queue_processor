using Api.GRRInnovations.TaskQueue.Processor.Domain.Entities;
using Api.GRRInnovations.TaskQueue.Processor.Domain.Models;
using Api.GRRInnovations.TaskQueue.Processor.Domain.Wrappers.In;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection;
using System.Text;

namespace Api.GRRInnovations.TaskQueue.Processor.Worker.Consumers
{
    public class TaskQueueConsumer : BackgroundService
    {
        private readonly ILogger<TaskQueueConsumer> _logger;
        private readonly ConnectionFactory _factory;
        private IChannel _channel;
        private IConnection _connection;
        private readonly ITaskService _taskService;

        public TaskQueueConsumer(ILogger<TaskQueueConsumer> logger, IOptions<RabbitMQSetting> rabbitMqSetting, ITaskService taskService)
        {
            _logger = logger;
            _factory = new ConnectionFactory
            {
                HostName = rabbitMqSetting.Value.HostName,
                UserName = rabbitMqSetting.Value.UserName,
                Password = rabbitMqSetting.Value.Password
            };

            _logger = logger;
            _taskService = taskService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Inicializando conexão com RabbitMQ...");

            _connection = await _factory.CreateConnectionAsync(stoppingToken);
            _channel = await _connection.CreateChannelAsync();

            await _channel.QueueDeclareAsync(queue: RabbitMQQueues.TaskQueue,
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

                bool processedSuccessfully = false;

                try
                {
                    processedSuccessfully = await ProcessMessageAsync(message);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception occurred while processing message from queue {RabbitMQQueues.TaskQueue}: {ex}");
                }

                if (processedSuccessfully)
                {
                    await _channel.BasicAckAsync(ea.DeliveryTag, false);
                }
                else
                {
                    await _channel.BasicRejectAsync(ea.DeliveryTag, requeue: true);
                }
                
                await Task.Delay(1000);
            };

            string consumerTag = await _channel.BasicConsumeAsync(RabbitMQQueues.TaskQueue, false, consumer);

            _logger.LogInformation("RabbitMQ Consumer iniciado. Aguardando mensagens...");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000, stoppingToken);
            }
        }

        private async Task<bool> ProcessMessageAsync(string message)
        {
            try
            {
                var model = System.Text.Json.JsonSerializer.Deserialize<WrapperInTask<TaskModel>>(message);
                var result = await model.Result().ConfigureAwait(false);

                await _taskService.CreateAsync(result);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing message: {ex.Message}");
                return false;
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
