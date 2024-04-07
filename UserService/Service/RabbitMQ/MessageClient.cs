using EasyNetQ;

namespace UserService.Service.RabbitMQ;

public class MessageClient
{
    private readonly IBus _bus;

    public MessageClient(IBus bus)
    {
        _bus = bus;
    }

    public void Listen<T>(Action<T> handler, string topic)
    {
        _bus.PubSub.Subscribe(topic, handler);
    }
    
    public void Publish<T>(T message, string topic)
    {
        _bus.PubSub.Publish(message, topic);
    }
}