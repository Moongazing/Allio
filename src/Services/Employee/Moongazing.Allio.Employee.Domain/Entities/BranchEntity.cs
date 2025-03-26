using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class BranchEntity : Entity<Guid>
{
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Country { get; set; } = default!;
    public CountryPhoneCode CountryPhoneCode { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public Guid DepartmentId { get; set; }
    public DepartmentEntity Department { get; set; } = default!;
    public ICollection<EmployeeEntity> Employees { get; set; } = new HashSet<EmployeeEntity>();
    public BranchEntity()
    {

    }


}
