using FluentValidation;

namespace Moongazing.Allio.Employee.Application.Features.BankDetails.Commands.Create;

public class CreateBankDetailCommandValidator : AbstractValidator<CreateBankDetailCommand>
{
    public CreateBankDetailCommandValidator()
    {

        RuleFor(x => x.BankName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.AccountNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(x => x.IBAN)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Matches(@"^[A-Z]{2}\d{2}[A-Z0-9]{1,30}$")
            .MaximumLength(34);

        RuleFor(x => x.Currency)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Length(3)
            .Matches("^[A-Z]{3}$");

        RuleFor(x => x.EmployeeId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}
