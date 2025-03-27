using Moongazing.Allio.Employee.Application.Features.Benefits.DataTransferObjects;
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


