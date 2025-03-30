using Moongazing.Kernel.Application.Responses;

namespace Moongazing.Allio.Employee.Application.Features.Departments.Commands.Create;

public class CreateDepartmentResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid DepartmentManagerId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}
