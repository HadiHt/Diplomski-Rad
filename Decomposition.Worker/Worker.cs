using AutoMapper;
using Decomposition.Worker.Dtos;
using Decomposition.Worker.Models;
using Decomposition.Worker.Services;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Decomposition.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        private IModel _channel;
        private const string QueueName = "Order_Receiver_API";

        public Worker(ILogger<Worker> logger, IMapper mapper, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
            InitializeRabbitMQListener();
        }

        private void InitializeRabbitMQListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Listening to RabbitMQ!");
            stoppingToken.Register(() => _logger.LogInformation("Stopping RabbitMQ listener..."));

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                if (body != null)
                {
                    var message = Encoding.UTF8.GetString(body);
                    var orderDto = JsonSerializer.Deserialize<OrderWriteDto>(message);
                    var order = _mapper.Map<Order>(orderDto);

                    // Create a new scope to resolve scoped services
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var messagesHandler = scope.ServiceProvider.GetRequiredService<MessagesHandler>();
                        await messagesHandler.HandleOrder(order);
                    }
                }
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}
