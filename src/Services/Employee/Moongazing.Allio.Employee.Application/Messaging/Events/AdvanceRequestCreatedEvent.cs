using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Messaging.Events;

namespace Moongazing.Allio.Employee.Application.Messaging.Events;

public class AdvanceRequestCreatedEvent:IEvent
{
    public Guid Id { get; } = Guid.NewGuid();  
    public DateTime CreatedAt { get; } = DateTime.UtcNow;  
    public Guid EmployeeId { get; set; }
    public decimal RequestedAmount { get; set; }
    public decimal ApprovedAmount { get; set; }
    public AdvanceRequestReason Reason { get; set; }
    public bool IsApproved { get; set; }
    public string? RejectReason { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? ApprovalDate { get; set; }
}
