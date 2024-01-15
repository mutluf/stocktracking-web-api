namespace StockTracking.Infrastructure.MessageBus
{
    public interface IMessageBus
    {
        void PublishMessage(string message, string gueueName);
        void Dispose();
    }
}
