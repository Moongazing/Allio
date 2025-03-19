using Moongazing.Kernel.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class TerminationEntity : Entity<Guid>
{
    public Guid EmployeeId { get; set; }
    public string Reason { get; set; } = default!;
    public DateTime TerminationDate { get; set; }
    public string Comments { get; set; } = default!;
    public bool IsVoluntary { get; set; }
    public virtual EmployeeEntity Employee { get; set; } = default!;

    public TerminationEntity()
    {

    }
}
