using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockTracking.Infrastructure.MessageBus;
using System.Threading.Channels;

namespace StockTracking.API
{
    public  class RabbitMQMessageConsumer: IDisposable
    {
        private IConnection connection;
        private IModel channel;
        private EventingBasicConsumer consumer;

        public RabbitMQMessageConsumer()
        {
              var factory = new ConnectionFactory() { HostName = "localhost" };

              connection = factory.CreateConnection();
              channel = connection.CreateModel();
            

            channel.QueueDeclare(queue: "hadi-rabbit", exclusive: false);


            consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received message: {message}");
            };
        }

        public void StartConsuming()
        {
            channel.BasicConsume(queue: "hadi-rabbit", autoAck: true, consumer: consumer);
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