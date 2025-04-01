using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class ProfessionEntity : Entity<Guid>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public ICollection<EmployeeEntity> Employees { get; set; } = new HashSet<EmployeeEntity>();
    public ProfessionEntity()
    {

    }

}