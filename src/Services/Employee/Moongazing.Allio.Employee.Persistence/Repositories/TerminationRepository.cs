using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Persistence.Contexts;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Persistence.Repositories;

public class TerminationRepository : EfRepositoryBase<TerminationEntity, Guid, EmployeeDbContext>, ITerminationRepository
{
    public TerminationRepository(EmployeeDbContext context) : base(context)
    {
    }
}