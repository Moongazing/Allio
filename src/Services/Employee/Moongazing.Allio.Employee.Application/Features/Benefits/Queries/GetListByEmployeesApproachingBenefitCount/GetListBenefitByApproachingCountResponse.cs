using Moongazing.Allio.Employee.Application.Features.Benefits.DataTransferObjects;
using Moongazing.Kernel.Application.Responses;

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetListByEmployeesApproachingBenefitCount;

public class GetListBenefitByApproachingCountResponse : IResponse
{
    public BenefitCountApproachingDto BenefitCountApproaching { get; set; } = default!;
}