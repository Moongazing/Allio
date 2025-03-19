using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class DepartmentEntity : Entity<Guid>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public ICollection<EmployeeEntity> Employees { get; set; } = new HashSet<EmployeeEntity>();
    public ICollection<BranchEntity> Branches { get; set; } = new HashSet<BranchEntity>();
    public DepartmentEntity()
    {

    }


}