using FluentValidation;

namespace Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Commands.Delete;

public class DeleteEmergencyContactCommandValidator:AbstractValidator<DeleteEmergencyContactCommand>
{
	public DeleteEmergencyContactCommandValidator()
	{
        RuleFor(x => x.Id)
         .Cascade(CascadeMode.Stop)
         .NotEmpty();
    }
}