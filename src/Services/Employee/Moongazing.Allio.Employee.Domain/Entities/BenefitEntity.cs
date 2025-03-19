using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class BenefitEntity : Entity<Guid>
{
    public string BenefitName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Value { get; set; }
    public Guid EmployeeId { get; set; }
    public virtual EmployeeEntity Employee { get; set; } = default!;

    public BenefitEntity()
    {

    }
}