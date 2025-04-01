using Microsoft.EntityFrameworkCore;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Rules;
using Moongazing.Kernel.CrossCuttingConcerns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongazing.Allio.Employee.Application.Features.Professions.Rules;

public class ProfesionBusinessRules : BaseBusinessRules
{
    private readonly IProfessionRepository proffesionRepository;

    public ProfesionBusinessRules(IProfessionRepository proffesionRepository)
    {
        this.proffesionRepository = proffesionRepository;
    }


    public void EnsureProffesionExists(ProfessionEntity? proffession)
    {
        if (proffession == null)
        {
            throw new BusinessException("Proffesion does not exist");
        }
    }
    public async Task EnsureNoEmployeesAssignedBeforeDelete(Guid professionId)
    {
        var profession = await proffesionRepository.GetAsync(p => p.Id == professionId, include: p => p.Include(p => p.Employees));
        if (profession?.Employees != null && profession.Employees.Count != 0)
        {
            throw new BusinessException("Cannot delete a profession assigned to employees.");
        }
    }
    public async Task EnsureProfessionNameIsUnique(string name)
    {
        bool exists = await proffesionRepository.AnyAsync(p => p.Name.ToLower() == name.ToLower());
        if (exists)
        {
            throw new BusinessException($"A profession with the name '{name}' already exists.");
        }
    }


}
