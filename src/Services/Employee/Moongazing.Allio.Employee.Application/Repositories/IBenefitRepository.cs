using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Application.Repositories;

public interface IBenefitRepository : IAsyncRepository<BenefitEntity, Guid>, IRepository<BenefitEntity, Guid>
{
    Task<decimal> GetTotalBenefitValueAsync(Guid employeeId, CancellationToken cancellationToken = default);
}
