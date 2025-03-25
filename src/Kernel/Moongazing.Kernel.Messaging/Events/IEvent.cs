namespace Moongazing.Kernel.Messaging.Events;

public interface IEvent
{
    Guid Id { get; }
    DateTime CreatedAt { get; }
}
