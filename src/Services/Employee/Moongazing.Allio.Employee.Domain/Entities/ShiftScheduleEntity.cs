using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class ShiftScheduleEntity : Entity<Guid>
{
    public Guid EmployeeId { get; set; }
    public DateTime ShiftStart { get; set; }
    public DateTime ShiftEnd { get; set; }
    public string Notes { get; set; } = default!;

    public virtual EmployeeEntity Employee { get; set; } = default!;

    public ShiftScheduleEntity()
    {

    }
}