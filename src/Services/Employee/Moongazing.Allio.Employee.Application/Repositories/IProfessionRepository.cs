using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Application.Repositories;

public interface IProfessionRepository : IAsyncRepository<ProfessionEntity, Guid>, IRepository<ProfessionEntity, Guid>
{
}
