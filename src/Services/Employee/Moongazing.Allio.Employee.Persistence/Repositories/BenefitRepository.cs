using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Persistence.Contexts;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Persistence.Repositories;

public class BenefitRepository : EfRepositoryBase<BenefitEntity, Guid, EmployeeDbContext>, IBenefitRepository
{
    public BenefitRepository(EmployeeDbContext context) : base(context)
    {
    }
}