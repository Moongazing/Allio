using Microsoft.EntityFrameworkCore;
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
    public async Task<decimal> GetTotalBenefitValueAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        return await Context.Benefits
            .Where(x => x.EmployeeId == employeeId && !x.DeletedDate.HasValue)
            .SumAsync(x => x.Value, cancellationToken);
    }

}