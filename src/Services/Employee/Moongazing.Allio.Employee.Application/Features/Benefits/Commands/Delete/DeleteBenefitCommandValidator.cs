using FluentValidation;

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Delete;

public class DeleteBenefitCommandValidator : AbstractValidator<DeleteBenefitCommand>
{
    public DeleteBenefitCommandValidator()
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}