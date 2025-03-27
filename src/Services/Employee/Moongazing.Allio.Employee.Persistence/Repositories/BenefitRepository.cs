using Microsoft.EntityFrameworkCore;
using Moongazing.Allio.Employee.Application.Features.Benefits.DataTransferObjects;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Persistence.Contexts;
using Moongazing.Kernel.Persistence.Paging;
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
    public async Task<IPaginate<BenefitLimitApproachingDto>> GetEmployeesWithHighBenefitUsageAsync(decimal threshold, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = Context.Benefits
            .Where(x => !x.DeletedDate.HasValue)
            .GroupBy(x => x.EmployeeId)
            .Select(g => new BenefitLimitApproachingDto
            {
                EmployeeId = g.Key,
                TotalBenefitValue = g.Sum(x => x.Value)
            })
            .Where(x => x.TotalBenefitValue >= threshold)
            .OrderByDescending(x => x.TotalBenefitValue);

        return await query.ToPaginateAsync(pageIndex, pageSize, cancellationToken);
    }

    public async Task<IPaginate<BenefitCountApproachingDto>> GetEmployeesApproachingBenefitCountLimitAsync(
    int threshold, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = Context.Benefits
            .Where(x => !x.DeletedDate.HasValue)
            .GroupBy(x => x.EmployeeId)
            .Select(g => new BenefitCountApproachingDto
            {
                EmployeeId = g.Key,
                BenefitCount = g.Count()
            })
            .Where(x => x.BenefitCount >= threshold)
            .OrderByDescending(x => x.BenefitCount);

        return await query.ToPaginateAsync(pageIndex, pageSize, cancellationToken);
    }


}