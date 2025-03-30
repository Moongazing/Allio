namespace Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetListByDynamic;

public class GetListBenefitByDynamicResponse
{
    public Guid Id { get; set; }
    public string BenefitName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Value { get; set; }
    public Guid EmployeeId { get; set; }
}
