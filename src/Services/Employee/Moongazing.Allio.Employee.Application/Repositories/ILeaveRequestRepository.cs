using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Application.Repositories;

public interface ILeaveRequestRepository : IAsyncRepository<LeaveRequestEntity, Guid>, IRepository<LeaveRequestEntity, Guid>
{
}
