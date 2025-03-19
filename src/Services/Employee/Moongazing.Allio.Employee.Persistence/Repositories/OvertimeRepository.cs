﻿using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Persistence.Contexts;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Allio.Employee.Persistence.Repositories;

public class OvertimeRepository : EfRepositoryBase<OvertimeEntity, Guid, EmployeeDbContext>, IOvertimeRepository
{
    public OvertimeRepository(EmployeeDbContext context) : base(context)
    {
    }
}