using Moongazing.Allio.Employee.Application.Features.BankDetails.Constants;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Rules;
using Moongazing.Kernel.CrossCuttingConcerns.Exceptions.Types;

namespace Moongazing.Allio.Employee.Application.Features.BankDetails.Rules;

public class BankDetailBusinessRules : BaseBusinessRules
{
    private readonly IBankDetailRepository bankDetailRepository;

    public BankDetailBusinessRules(IBankDetailRepository bankDetailRepository)
    {
        this.bankDetailRepository = bankDetailRepository;
    }

    public async Task EnsureUniqueIBAN(string iban)
    {
        iban = iban.ToLower();
        var bankDetail = await bankDetailRepository.AnyAsync(predicate: x => x.IBAN.ToLower() == iban, withDeleted: false);
        if (bankDetail)
        {
            throw new BusinessException(BankDetailConstants.IBANShouldBeUnique);
        }
    }

    public void EnsureBankDetailExists(BankDetailEntity? bankDetail)
    {
        if (bankDetail == null)
        {
            throw new BusinessException(BankDetailConstants.BankDetailNotFound);
        }
    }
    public async Task EnsureBankDetailExists(Guid bankDetailId)
    {
        var bankDetail = await bankDetailRepository.AnyAsync(predicate: x => x.Id == bankDetailId, withDeleted: false);
        if (!bankDetail)
        {
            throw new BusinessException(BankDetailConstants.BankDetailNotFound);
        }
    }
    public async Task EnsureAccountNumberExists(string accountNumber)
    {
        var bankDetail = await bankDetailRepository.AnyAsync(predicate: x => x.AccountNumber == accountNumber, withDeleted: false);
        if (bankDetail)
        {
            throw new BusinessException(BankDetailConstants.AccountNumberShouldBeUnique);
        }
    }


}
