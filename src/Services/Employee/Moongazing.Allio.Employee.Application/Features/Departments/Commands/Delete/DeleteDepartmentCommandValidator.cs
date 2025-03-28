using FluentValidation;

namespace Moongazing.Allio.Employee.Application.Features.Departments.Commands.Delete;

public class DeleteDepartmentCommandValidator:AbstractValidator<DeleteDepartmentCommand>
{
    public DeleteDepartmentCommandValidator()
    {
        RuleFor(x => x.Id)
           .Cascade(CascadeMode.Stop)
           .NotEmpty();
    }
}