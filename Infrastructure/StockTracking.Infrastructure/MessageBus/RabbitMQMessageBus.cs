using RabbitMQ.Client;
using System.Text;

namespace StockTracking.Infrastructure.MessageBus
{
    public class RabbitMQMessageBus:IMessageBus, IDisposable
    {
        private readonly IConnection connection;
        private readonly IModel channel;

        public RabbitMQMessageBus()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        public void PublishMessage(string message, string gueueName)
        {
            channel.QueueDeclare(queue: gueueName, exclusive: false);

            var body = Encoding.UTF8.GetBytes(message);


            channel.BasicPublish(exchange: ExchangeType.Direct,
                routingKey: gueueName, 
                                 basicProperties: null,
                                 body: body);
        }
        public void Dispose()
        {
            channel?.Close();
            connection?.Close();
            channel?.Dispose();
            connection?.Dispose();
        }

    }
}
