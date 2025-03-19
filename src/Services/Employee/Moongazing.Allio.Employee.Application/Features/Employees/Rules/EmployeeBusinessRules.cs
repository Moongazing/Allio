using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Kernel.Application.Rules;
using Moongazing.Kernel.CrossCuttingConcerns.Exceptions.Types;

namespace Moongazing.Allio.Employee.Application.Features.Employees.Rules;

public class EmployeeBusinessRules : BaseBusinessRules
{
    private readonly IEmployeeRepository employeeRepository;

    public EmployeeBusinessRules(IEmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }


    public async Task EnsureEmployeeExistsAsync(Guid employeeId)
    {
        var employee = await employeeRepository.AnyAsync(predicate: x => x.Id == employeeId, withDeleted: false);
        if (!employee)
        {
            throw new BusinessException("Employee does not exist.");
        }

    }

}
