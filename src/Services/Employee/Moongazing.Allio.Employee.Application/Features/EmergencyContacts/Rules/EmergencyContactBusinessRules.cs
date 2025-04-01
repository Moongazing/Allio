using Microsoft.EntityFrameworkCore;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Application.Rules;
using Moongazing.Kernel.CrossCuttingConcerns.Exceptions.Types;

namespace Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Rules;

public class EmergencyContactBusinessRules : BaseBusinessRules
{
    private readonly IEmergencyContactRepository emergencyContactRepository;

    public EmergencyContactBusinessRules(IEmergencyContactRepository emergencyContactRepository)
    {
        this.emergencyContactRepository = emergencyContactRepository;
    }
    public void EnsureEmergencyContanctExists(EmergencyContactEntity? emergencyContact)
    {
        if (emergencyContact == null)
        {
            throw new BusinessException("Emergency contact does not exist");
        }
    }
    public async Task EnsurePhoneNumberIsUnique(string phoneNumber)
    {
        var emergencyContact = await emergencyContactRepository.AnyAsync(predicate: x => x.PhoneNumber == phoneNumber);
        if (emergencyContact)
        {
            throw new BusinessException("Phone number already exists");
        }
    }
    public async Task EnsureEmployeeDoesNotHaveDuplicatePhoneNumber(Guid employeeId, string phoneNumber)
    {
        bool exists = await emergencyContactRepository.AnyAsync(
            predicate: x => x.EmployeeId == employeeId && x.PhoneNumber == phoneNumber);

        if (exists)
            throw new BusinessException("This employee already has an emergency contact with this phone number.");
    }
    public async Task EnsureEmployeeHasNotExceededMaxEmergencyContacts(Guid employeeId, int maxAllowed = 3)
    {
        int count = await emergencyContactRepository
            .Query()
            .Where(x => x.EmployeeId == employeeId)
            .CountAsync();

        if (count >= maxAllowed)
            throw new BusinessException($"An employee can have at most {maxAllowed} emergency contacts.");
    }

    public void EnsureCountryPhoneCodeIsValid(CountryPhoneCode code)
    {
        if (!Enum.IsDefined(typeof(CountryPhoneCode), code))
            throw new BusinessException("Invalid country phone code.");
    }


}
