using Moongazing.Allio.Employee.Application.Features.BankDetails.Constants;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Rules;
using Moongazing.Kernel.CrossCuttingConcerns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        var bankDetail = await bankDetailRepository.GetAsync(predicate: x => x.IBAN.ToLower() == iban, withDeleted: false);
        if (bankDetail != null)
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
    public async Task EnsureAccountNumberExists(string accountNumber)
    {
        var bankDetail = await bankDetailRepository.GetAsync(predicate: x => x.AccountNumber == accountNumber, withDeleted: false);
        if (bankDetail != null)
        {
            throw new BusinessException(BankDetailConstants.AccountNumberShouldBeUnique);
        }
    }


}
