using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class PayrollEntity : Entity<Guid>
{
    public Guid EmployeeId { get; set; }
    public DateTime PayDate { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal NetSalary { get; set; }
    public decimal TaxDeduction { get; set; }
    public decimal Bonus { get; set; }

    public virtual EmployeeEntity Employee { get; set; } = default!;

    public PayrollEntity()
    {
    }
}