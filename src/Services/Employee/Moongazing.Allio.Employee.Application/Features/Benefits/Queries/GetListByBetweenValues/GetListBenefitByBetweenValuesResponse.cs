using Moongazing.Kernel.Application.Responses;

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetListByBetweenValues;

public class GetListBenefitByBetweenValuesResponse : IResponse
{
    public Guid Id { get; set; }
    public string BenefitName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Value { get; set; }
    public Guid EmployeeId { get; set; }
}