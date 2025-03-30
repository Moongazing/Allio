using FluentValidation;

namespace Moongazing.Allio.Employee.Application.Features.Departments.Commands.Update;

public class UpdateDepartmentCommandValidator:AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(x => x.Id)
         .Cascade(CascadeMode.Stop)
         .NotEmpty();

        RuleFor(x => x.Name)
           .Cascade(CascadeMode.Stop)
           .NotEmpty()
           .MaximumLength(100);

        RuleFor(x => x.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(100);
    }
}