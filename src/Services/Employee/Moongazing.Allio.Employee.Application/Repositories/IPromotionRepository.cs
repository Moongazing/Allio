using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Application.Repositories;

public interface IPromotionRepository : IAsyncRepository<PromotionEntity, Guid>, IRepository<PromotionEntity, Guid>
{
}
