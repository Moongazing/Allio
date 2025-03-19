using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Persistence.Contexts;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Persistence.Repositories;

public class EmployeeRepository : EfRepositoryBase<EmployeeEntity, Guid, EmployeeDbContext>, IEmployeeRepository
{
    public EmployeeRepository(EmployeeDbContext context) : base(context)
    {
    }
}