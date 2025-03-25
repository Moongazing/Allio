using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Persistence.Paging;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Application.Repositories;

public interface IBenefitRepository : IAsyncRepository<BenefitEntity, Guid>, IRepository<BenefitEntity, Guid>
{
    Task<decimal> GetTotalBenefitValueAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task<IPaginate<BenefitLimitApproachingDto>> GetEmployeesWithHighBenefitUsageAsync(decimal threshold, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<IPaginate<BenefitCountApproachingDto>> GetEmployeesApproachingBenefitCountLimitAsync(
    int threshold, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

}


public class BenefitLimitApproachingDto
{
    public Guid EmployeeId { get; set; }
    public decimal TotalBenefitValue { get; set; }
}

public class BenefitCountApproachingDto
{
    public Guid EmployeeId { get; set; }
    public int BenefitCount { get; set; }
}
