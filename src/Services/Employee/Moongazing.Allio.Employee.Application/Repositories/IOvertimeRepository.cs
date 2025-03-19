using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Application.Repositories;

public interface IOvertimeRepository : IAsyncRepository<OvertimeEntity, Guid>, IRepository<OvertimeEntity, Guid>
{
}
