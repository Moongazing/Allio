using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Persistence.Paging;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Application.Repositories;

public interface IBenefitRepository : IAsyncRepository<BenefitEntity, Guid>, IRepository<BenefitEntity, Guid>
{
    Task<decimal> GetTotalBenefitValueAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task<IPaginate<BenefitLimitApproachingDto>> GetEmployeesWithHighBenefitUsageAsync(decimal threshold, int pageIndex, int pageSize, CancellationToken cancellationToken);
}


public class BenefitLimitApproachingDto
{
    public Guid EmployeeId { get; set; }
    public decimal TotalBenefitValue { get; set; }
}

