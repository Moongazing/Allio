﻿using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Domain.Entities;

public class EmergencyContactEntity : Entity<Guid>
{
    public string Name { get; set; } = default!;
    public CountryPhoneCode CountryPhoneCode { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string Relation { get; set; } = default!;
    public Guid EmployeeId { get; set; }
    public virtual EmployeeEntity Employee { get; set; } = default!;
    public EmergencyContactEntity()
    {

    }
}