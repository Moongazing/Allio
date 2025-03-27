using Moongazing.Kernel.Application.Responses;

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetListByEmployee;

public class GetListBenefitByEmployeeIdResponse:IResponse
{
    public Guid Id { get; set; }
    public string BenefitName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Value { get; set; }
    public Guid EmployeeId { get; set; }
}
