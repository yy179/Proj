using Project.Abstractions.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Services
{
    public class MessageQueueService : IMessageQueueService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageQueueService()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "chat_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: "chat_queue", basicProperties: null, body: body);
        }

        public void StartReceivingMessages(Action<string> onMessageReceived)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                onMessageReceived(message);
            };
            _channel.BasicConsume(queue: "chat_queue", autoAck: true, consumer: consumer);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
