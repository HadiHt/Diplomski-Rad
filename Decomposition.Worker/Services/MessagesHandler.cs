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
        private readonly CamundaService _camundaService;
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "CamundaQueue";

        public MessagesHandler(IMapper mapper, ILogger<MessagesHandler> logger, CamundaService camundaService)
        {
            _mapper = mapper;
            _logger = logger;
            _camundaService = camundaService;
        }

        public async Task<bool> HandleOrder(Order order)
        {
            Console.WriteLine($"Received Order: {order.OrderId}");

            var orderReadDto = _mapper.Map<OrderReadDto>(order);
            orderReadDto.Woid = (await _camundaService.GetNextWoidAsync()).ToString();

            PublishOrderReadDto(orderReadDto);
            return true;
        }

        public void PublishOrderReadDto(OrderReadDto orderReadDto)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                UserName = "guest",
                Password = "guest"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var message = JsonSerializer.Serialize(orderReadDto);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: _queueName,
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($"Published message to {_queueName}: {message}");
            }
        }
    }

}
