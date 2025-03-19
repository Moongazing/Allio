using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Persistence.Contexts;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Persistence.Repositories;

public class BranchRepository : EfRepositoryBase<BranchEntity, Guid, EmployeeDbContext>, IBranchRepository
{
    public BranchRepository(EmployeeDbContext context) : base(context)
    {
    }
}