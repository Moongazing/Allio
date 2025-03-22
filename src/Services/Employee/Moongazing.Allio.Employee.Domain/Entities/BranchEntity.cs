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


public enum CountryPhoneCode
{
    US = 1,
    UK = 44,
    DE = 49,
    FR = 33,
    IT = 39,
    TR = 90,
    IN = 91,
    CN = 86,
    JP = 81,
    BR = 55,
    RU = 7,
    AU = 61,
    MX = 52,
    KR = 82,
    ES = 34,
    ID = 62,
    NL = 31,

}