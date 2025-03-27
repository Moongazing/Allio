using Moongazing.Kernel.Application.DataTransferObjects;

namespace Moongazing.Allio.Employee.Application.Features.Benefits.DataTransferObjects;

public class BenefitLimitApproachingDto : IDto
{
    public Guid EmployeeId { get; set; }
    public decimal TotalBenefitValue { get; set; }
}