using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class DocumentEntity : Entity<Guid>
{
    public string DocumentName { get; set; } = default!;
    public string DocumentUrl { get; set; } = default!;
    public DateTime UploadedAt { get; set; }
    public Guid EmployeeId { get; set; }
    public DocumentType Type { get; set; }
    public virtual EmployeeEntity Employee { get; set; } = default!;

    public DocumentEntity()
    {

    }
}