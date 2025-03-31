using Moongazing.Kernel.Application.Responses;

namespace Moongazing.Allio.Employee.Application.Features.Departments.Queries.GetList;

public class GetListDepartmentResponse:IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid DepartmentManagerId { get; set; } = default!;
}