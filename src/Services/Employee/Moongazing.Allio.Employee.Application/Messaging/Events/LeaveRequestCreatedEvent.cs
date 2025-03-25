using Moongazing.Kernel.Messaging.Events;

namespace Moongazing.Allio.Employee.Application.Messaging.Events;

public class LeaveRequestCreatedEvent : IEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public Guid EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string LeaveType { get; set; } = default!;
}
