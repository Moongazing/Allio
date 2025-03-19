using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class PromotionEntity : Entity<Guid>
{

    public Guid EmployeeId { get; set; }
    public string NewPosition { get; set; } = default!;
    public DateTime PromotionDate { get; set; }
    public string ApprovedBy { get; set; } = default!;
    public string Comments { get; set; } = default!;
    public virtual EmployeeEntity Employee { get; set; } = default!;

    public PromotionEntity()
    {

    }

}