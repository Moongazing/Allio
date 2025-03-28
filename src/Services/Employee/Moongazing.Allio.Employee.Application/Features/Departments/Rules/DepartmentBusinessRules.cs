using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Rules;
using Moongazing.Kernel.CrossCuttingConcerns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongazing.Allio.Employee.Application.Features.Departments.Rules;

public class DepartmentBusinessRules : BaseBusinessRules
{
    private readonly IDepartmentRepository departmentRepository;

    public DepartmentBusinessRules(IDepartmentRepository departmentRepository)
    {
        this.departmentRepository = departmentRepository;
    }

    public async Task EnsureDepartmentNameIsUnique(string name)
    {
        name = name.ToLower();
        var department = await departmentRepository.AnyAsync(predicate: x => x.Name.ToLower() == name);
        if (department)
        {
            throw new BusinessException("Department");
        }
    }

    public void EnsureDepartmentExists(DepartmentEntity? department)
    {
        if (department == null)
        {
            throw new BusinessException("Department");
        }
    }
}
