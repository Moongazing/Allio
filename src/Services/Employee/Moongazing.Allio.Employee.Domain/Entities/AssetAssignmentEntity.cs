using Moongazing.Kernel.Persistence.Repositories;
using System;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class AssetAssignmentEntity : Entity<Guid>
{
    public Guid EmployeeId { get; set; }
    public string AssetName { get; set; } = default!;
    public string AssetType { get; set; } = default!;
    public string? SerialNumber { get; set; } 
    public DateTime AssignedDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
    public string? Notes { get; set; }
    public bool IsReturned => ReturnedDate.HasValue;

    public virtual EmployeeEntity Employee { get; set; } = default!;
    public AssetAssignmentEntity()
    {

    }
}
