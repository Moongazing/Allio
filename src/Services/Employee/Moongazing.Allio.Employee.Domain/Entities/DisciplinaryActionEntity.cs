using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class DisciplinaryActionEntity : Entity<Guid>
{
    public Guid EmployeeId { get; set; }
    public string ActionType { get; set; } = default!;
    public string Reason { get; set; } = default!;
    public DateTime ActionDate { get; set; }
    public string Comments { get; set; } = default!;

    public virtual EmployeeEntity Employee { get; set; } = default!;

    public DisciplinaryActionEntity()
    {

    }

}