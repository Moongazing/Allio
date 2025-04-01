using FluentValidation;

namespace Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Commands.Update;

public class UpdateEmergencyContactCommandValidator : AbstractValidator<UpdateEmergencyContactCommand>
{
    public UpdateEmergencyContactCommandValidator()
    {
        RuleFor(x => x.Id)
         .Cascade(CascadeMode.Stop)
         .NotEmpty();

        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Relation)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.EmployeeId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(x => x.CountryPhoneCode)
            .Cascade(CascadeMode.Stop)
            .IsInEnum();
    }
}