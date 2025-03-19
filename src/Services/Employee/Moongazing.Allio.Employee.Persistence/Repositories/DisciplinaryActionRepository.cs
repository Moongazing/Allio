using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Persistence.Contexts;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Persistence.Repositories;

public class DisciplinaryActionRepository : EfRepositoryBase<DisciplinaryActionEntity, Guid, EmployeeDbContext>, IDisciplinaryActionRepository
{
    public DisciplinaryActionRepository(EmployeeDbContext context) : base(context)
    {
    }
}