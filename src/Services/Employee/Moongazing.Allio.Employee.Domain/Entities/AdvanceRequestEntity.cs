using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class AdvanceRequestEntity : Entity<Guid>
{
    public Guid EmployeeId { get; set; }
    public decimal RequestedAmount { get; set; } //max salary %40
    public decimal ApprovedAmount { get; set; }
    public AdvanceRequestReason Reason { get; set; } = default!;
    public bool IsApproved { get; set; } = false;
    public string? RejectReason { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? ApprovalDate { get; set; }

    public virtual EmployeeEntity Employee { get; set; } = default!;

    public AdvanceRequestEntity()
    {

    }

}