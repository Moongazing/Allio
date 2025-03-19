using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class AttendanceEntity : Entity<Guid>
{
    public Guid EmployeeId { get; set; }
    public DateTime CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public AttendanceStatus Status { get; set; } = default!;

    public virtual EmployeeEntity Employee { get; set; } = default!;

    public AttendanceEntity()
    {

    }
}