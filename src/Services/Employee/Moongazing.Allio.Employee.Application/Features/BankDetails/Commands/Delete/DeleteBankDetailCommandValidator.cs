using FluentValidation;

namespace Moongazing.Allio.Employee.Application.Features.BankDetails.Commands.Delete;

public class DeleteBankDetailCommandValidator : AbstractValidator<DeleteBankDetailCommand>
{
    public DeleteBankDetailCommandValidator()
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}