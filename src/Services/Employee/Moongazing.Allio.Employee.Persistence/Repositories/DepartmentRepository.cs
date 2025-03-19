using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Persistence.Contexts;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Persistence.Repositories;

public class DepartmentRepository : EfRepositoryBase<DepartmentEntity, Guid, EmployeeDbContext>, IDepartmentRepository
{
    public DepartmentRepository(EmployeeDbContext context) : base(context)
    {
    }
}