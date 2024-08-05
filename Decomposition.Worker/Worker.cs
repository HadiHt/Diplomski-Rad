using AutoMapper;
using Decomposition.Worker.Dtos;
using Decomposition.Worker.Models;
using Decomposition.Worker.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Decomposition.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly MessagesHandler _messagesHandler;
        private readonly IMapper _mapper;
        private IConnection _connection;
        private IModel _channel;
        private const string QueueName = "Order_Receiver_API";

        public Worker(ILogger<Worker> logger, MessagesHandler messagesHandler, IMapper mapper)
        {
            _logger = logger;
            _messagesHandler = messagesHandler;
            _mapper = mapper;
            InitializeRabbitMQListener();
            _mapper = mapper;
        }

        private void InitializeRabbitMQListener()
        {
            var factory = new ConnectionFactory { 
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
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                if (body != null)
                {
                    var message = Encoding.UTF8.GetString(body);
                    var orderDto = JsonSerializer.Deserialize<OrderWriteDto>(message);
                    var order = _mapper.Map<Order>(orderDto);
                    _messagesHandler.HandleOrder(order);
                }
                // Acknowledge the message
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken); // Adding a delay to avoid tight loop
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
