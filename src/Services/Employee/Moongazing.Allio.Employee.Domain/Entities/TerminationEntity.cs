using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class TerminationEntity : Entity<Guid>
{
    public Guid EmployeeId { get; set; }
    public string Reason { get; set; } = default!;
    public DateTime TerminationDate { get; set; }
    public string Comments { get; set; } = default!;
    public bool IsVoluntary { get; set; }
    public virtual EmployeeEntity Employee { get; set; } = default!;

    public TerminationEntity()
    {

    }
}
