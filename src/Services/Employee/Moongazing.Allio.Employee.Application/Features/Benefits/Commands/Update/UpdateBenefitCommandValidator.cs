using FluentValidation;

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Update;

public class UpdateBenefitCommandValidator:AbstractValidator<UpdateBenefitCommand>
{
    public UpdateBenefitCommandValidator()
    {
        RuleFor(x => x.Id)
          .Cascade(CascadeMode.Stop)
          .NotEmpty();

        RuleFor(x => x.BenefitName)
          .Cascade(CascadeMode.Stop)
          .NotEmpty()
          .MaximumLength(100);

        RuleFor(x => x.Description)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(250);

        RuleFor(x => x.Value)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0);

        RuleFor(x => x.EmployeeId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}