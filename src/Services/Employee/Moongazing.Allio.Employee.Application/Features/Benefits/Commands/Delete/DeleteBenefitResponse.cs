using Moongazing.Kernel.Application.Responses;

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Delete;

public class DeleteBenefitResponse : IResponse
{
    public Guid Id { get; set; }
}