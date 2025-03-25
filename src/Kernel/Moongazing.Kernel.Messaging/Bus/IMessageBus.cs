namespace Moongazing.Kernel.Messaging.Bus;

public interface IMessageBus
{
    Task PublishAsync<T>(T @event) where T : class;
    Task SendAsync<T>(string queueName, T message) where T : class;
}

