﻿using Moongazing.Kernel.Application.Responses;

namespace Moongazing.Allio.Employee.Application.Features.Departments.Queries.GetByName;

public class GetDepartmentByNameResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid DepartmentManagerId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}