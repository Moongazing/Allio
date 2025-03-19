using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class OvertimeEntity : Entity<Guid>
{
    public Guid EmployeeId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal HourlyRate { get; set; }
    public decimal TotalAmount { get; set; }
    public virtual EmployeeEntity Employee { get; set; } = default!;

    public OvertimeEntity()
    {

    }

}