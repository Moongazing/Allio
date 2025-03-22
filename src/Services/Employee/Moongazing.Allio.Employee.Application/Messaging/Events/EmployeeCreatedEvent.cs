using Moongazing.Kernel.Messaging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongazing.Allio.Employee.Application.Messaging.Events;

public class EmployeeCreatedEvent : IEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public Guid EmployeeId { get; set; }
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
}
