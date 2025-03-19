using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Application.Repositories;

public interface IDocumentRepository : IAsyncRepository<DocumentEntity, Guid>, IRepository<DocumentEntity, Guid>
{
}
