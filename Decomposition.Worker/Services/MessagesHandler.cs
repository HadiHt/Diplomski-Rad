using AutoMapper;
using Decomposition.Worker.Dtos;
using Decomposition.Worker.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Decomposition.Worker.Services
{
    public class MessagesHandler
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MessagesHandler> _logger;
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "CamundaQueue";
        public MessagesHandler(IMapper mapper, ILogger<MessagesHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }
        public void HandleOrder(Order order)
        {
            Console.WriteLine($"Received Order: {order.OrderId}");

            var orderReadDto = _mapper.Map<OrderReadDto>(order);
            PublishOrderReadDto(orderReadDto);
        }


        public void PublishOrderReadDto(OrderReadDto orderReadDto)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declare the queue (create if not exists)
                channel.QueueDeclare(queue: _queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Serialize the OrderReadDto to JSON
                var message = JsonSerializer.Serialize(orderReadDto);
                var body = Encoding.UTF8.GetBytes(message);

                // Publish the message
                channel.BasicPublish(exchange: "",
                                     routingKey: _queueName,
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($"Published message to {_queueName}: {message}");
            }
        }
    }
}
