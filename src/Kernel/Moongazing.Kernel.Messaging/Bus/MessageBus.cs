using MassTransit;
using Microsoft.Extensions.Logging;

namespace Moongazing.Kernel.Messaging.Bus;

public class MessageBus : IMessageBus
{
    private readonly IBus bus;
    private readonly ILogger<MessageBus> logger;

    public MessageBus(IBus bus, ILogger<MessageBus> logger)
    {
        this.bus = bus;
        this.logger = logger;
    }

    public async Task PublishAsync<T>(T @event) where T : class
    {
        logger.LogInformation($"Publishing event: {typeof(T).Name}");
        await bus.Publish(@event);
    }

    public async Task SendAsync<T>(string queueName, T message) where T : class
    {
        logger.LogInformation($"Sending message to queue {queueName}: {typeof(T).Name}");
        var sendEndpoint = await bus.GetSendEndpoint(new Uri($"queue:{queueName}"));
        await sendEndpoint.Send(message);
    }
}
