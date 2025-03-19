using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class PerformanceReviewEntity : Entity<Guid>
{
    public Guid EmployeeId { get; set; }
    public DateTime ReviewDate { get; set; }
    public string Reviewer { get; set; } = default!;
    public int Score { get; set; }
    public string Comments { get; set; } = default!;

    public virtual EmployeeEntity Employee { get; set; } = default!;

    public PerformanceReviewEntity()
    {

    }

}