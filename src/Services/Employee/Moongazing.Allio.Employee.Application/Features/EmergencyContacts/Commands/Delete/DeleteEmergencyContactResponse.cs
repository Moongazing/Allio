using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Application.Responses;

namespace Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Commands.Delete;

public class DeleteEmergencyContactResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public CountryPhoneCode CountryPhoneCode { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string Relation { get; set; } = default!;
    public Guid EmployeeId { get; set; }
}
