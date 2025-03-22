using Moongazing.Kernel.Messaging.Events;

namespace Moongazing.Allio.Employee.Application.Messaging.Events;

public class ExpenseRecordCreatedEvent:IEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public Guid EmployeeId { get; set; }
    public string ExpenseType { get; set; } = default!;
    public decimal Amount { get; set; }
    public string Description { get; set; } = default!;
    public DateTime ExpenseDate { get; set; }
    public bool IsReimbursed { get; set; } = false;
    public string DocumentUrl { get; set; } = default!;
}