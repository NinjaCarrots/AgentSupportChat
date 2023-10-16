using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportChat.Core.Infrastructure
{
    public class RabbitMQProducer
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQProducer(IConfiguration configuration)
        {
            _configuration = configuration;

            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:Host"],
                Port = int.Parse(_configuration["RabbitMQ:Port"]),
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"]
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _configuration["RabbitMQ:QueueName"], durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void SendMessage(string messageContent)
        {
            var message = Encoding.UTF8.GetBytes(messageContent);
            _channel.BasicPublish(exchange: "", routingKey: _configuration["RabbitMQ:QueueName"], basicProperties: null, body: message);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
