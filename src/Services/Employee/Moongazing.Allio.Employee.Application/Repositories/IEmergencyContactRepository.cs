using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Application.Repositories;

public interface IEmergencyContactRepository : IAsyncRepository<EmergencyContactEntity, Guid>, IRepository<EmergencyContactEntity, Guid>
{
}
