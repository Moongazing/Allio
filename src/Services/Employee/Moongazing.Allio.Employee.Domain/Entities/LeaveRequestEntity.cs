using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class LeaveRequestEntity : Entity<Guid>
{
    public Guid EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LeaveType LeaveType { get; set; } = default!;
    public string Reason { get; set; } = default!;
    public bool IsApproved { get; set; } = false;
    public DateTime RequestDate { get; set; }
    public string ApprovedBy { get; set; } = default!;

    public virtual EmployeeEntity Employee { get; set; } = default!;

    public LeaveRequestEntity()
    {

    }

}