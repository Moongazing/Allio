using FluentValidation;

public class DeleteBankDetailCommandValidator:AbstractValidator<DeleteBankDetailCommand>
{
    public DeleteBankDetailCommandValidator()
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}