using EasyNetQ;
using Shared.Monitoring;

namespace AuthService.Service.RabbitMQ;

public class MessageClient
{
    private readonly IBus _bus;

    public MessageClient(IBus bus)
    {
        _bus = bus;
    }

    public void Listen<T>(Action<T> handler, string topic)
    {
        Monitoring.Log.Debug("Auth - Listening to topic: " + topic);
        _bus.PubSub.Subscribe(topic, handler);
    }
    
    public void Publish<T>(T message, string topic)
    {
        Monitoring.Log.Debug("Auth - Publishing to topic: " + topic);
        _bus.PubSub.Publish(message, topic);
    }
}