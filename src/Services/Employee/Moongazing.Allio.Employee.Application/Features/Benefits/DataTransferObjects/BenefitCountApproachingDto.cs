using Moongazing.Kernel.Application.DataTransferObjects;

namespace Moongazing.Allio.Employee.Application.Features.Benefits.DataTransferObjects;

public class BenefitCountApproachingDto : IDto
{
    public Guid EmployeeId { get; set; }
    public int BenefitCount { get; set; }
}