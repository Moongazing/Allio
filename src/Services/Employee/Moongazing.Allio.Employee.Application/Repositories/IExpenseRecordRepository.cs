using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Application.Repositories;

public interface IExpenseRecordRepository : IAsyncRepository<ExpenseRecordEntity, Guid>, IRepository<ExpenseRecordEntity, Guid>
{
}
