using Moongazing.Kernel.Application.Responses;

namespace Moongazing.Allio.Employee.Application.Features.Departments.Commands.Update;

public class UpdateDepartmentResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid DepartmentManagerId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}
