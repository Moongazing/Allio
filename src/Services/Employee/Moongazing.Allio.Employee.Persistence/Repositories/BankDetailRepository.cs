using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Persistence.Contexts;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Persistence.Repositories
{
    public class BankDetailRepository : EfRepositoryBase<BankDetailEntity, Guid, EmployeeDbContext>, IBankDetailRepository
    {
        public BankDetailRepository(EmployeeDbContext context) : base(context)
        {
        }
    }
}