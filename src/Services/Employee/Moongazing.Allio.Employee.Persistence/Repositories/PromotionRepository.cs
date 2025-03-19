using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Persistence.Contexts;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Persistence.Repositories;

public class PromotionRepository : EfRepositoryBase<PromotionEntity, Guid, EmployeeDbContext>, IPromotionRepository
{
    public PromotionRepository(EmployeeDbContext context) : base(context)
    {
    }
}