using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class BankDetailEntity : Entity<Guid>
{
    public string BankName { get; set; } = default!;
    public string AccountNumber { get; set; } = default!;
    public string IBAN { get; set; } = default!;
    public Currency Currency { get; set; }
    public Guid EmployeeId { get; set; }
    public virtual EmployeeEntity Employee { get; set; } = default!;

    public BankDetailEntity()
    {

    }
}