using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class ExpenseRecordEntity : Entity<Guid>
{
    public Guid EmployeeId { get; set; }
    public ExpenseType ExpenseType { get; set; } = default!;
    public decimal Amount { get; set; }
    public string Description { get; set; } = default!;
    public DateTime ExpenseDate { get; set; }
    public bool IsReimbursed { get; set; } = false;
    public Guid DocumentId { get; set; } = default!;
    public virtual DocumentEntity Document { get; set; } = default!;
    public virtual EmployeeEntity Employee { get; set; } = default!;

    public ExpenseRecordEntity()
    {

    }

}