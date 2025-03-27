using Moongazing.Allio.Employee.Application.Features.Benefits.DataTransferObjects;
using Moongazing.Kernel.Application.Responses;

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetListByEmployeesApproachingBenefitLimit;

public class GetListBenefitByApproachingLimitResponse : IResponse
{
    public BenefitLimitApproachingDto BenefitLimitApproaching { get; set; } = default!;
}