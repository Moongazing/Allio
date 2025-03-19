using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Persistence.Contexts;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Persistence.Repositories;

public class ExpenseRecordRepository : EfRepositoryBase<ExpenseRecordEntity, Guid, EmployeeDbContext>, IExpenseRecordRepository
{
    public ExpenseRecordRepository(EmployeeDbContext context) : base(context)
    {
    }
}