using Microsoft.EntityFrameworkCore;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Kernel.Application.Rules;
using Moongazing.Kernel.CrossCuttingConcerns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Rules;

public class BenefitBusinessRules : BaseBusinessRules
{
    private readonly IBenefitRepository benefitRepository;

    public BenefitBusinessRules(IBenefitRepository benefitRepository)
    {
        this.benefitRepository = benefitRepository;
    }

    public async Task EnsureEmployeeDoesNotHaveBenefit(Guid employeeId, string benefitName)
    {
        benefitName = benefitName.ToLower();
        var benefit = await benefitRepository.AnyAsync(predicate: x => x.EmployeeId == employeeId && x.BenefitName.ToLower() == benefitName, withDeleted: false);
        if (benefit)
        {
            throw new BusinessException("Employee already has the same benefit");
        }
    }
    public async Task EnsureBenefitLimitNotExceeded(Guid employeeId, decimal newBenefitValue)
    {
        var total = await benefitRepository.GetTotalBenefitValueAsync(employeeId);

        if ((total + newBenefitValue) > 10000)
        {
            throw new BusinessException("Total benefit limit per employee exceeded.");
        }
    }
    public async Task EnsureMaxBenefitCountNotExceededPerEmployee(Guid employeeId)
    {
        var count = await benefitRepository.Query()
                                           .Where(x => x.EmployeeId == employeeId && !x.DeletedDate.HasValue)
                                           .CountAsync();

        if (count >= 5)
            throw new BusinessException("An employee cannot have more than 5 benefits.");
    }



}
