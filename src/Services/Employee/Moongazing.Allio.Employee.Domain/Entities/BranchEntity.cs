using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class BranchEntity : Entity<Guid>
{
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public Guid DepartmentId { get; set; }
    public DepartmentEntity Department { get; set; } = default!;
    public ICollection<EmployeeEntity> Employees { get; set; } = new HashSet<EmployeeEntity>();
    public BranchEntity()
    {

    }


}