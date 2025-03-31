using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Rules;
using Moongazing.Allio.Employee.Application.Features.Employees.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Pipelines.Transaction;

namespace Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Commands.Create;

public class CreateEmergencyContactCommand : IRequest<CreateEmergencyContactResponse>,
       ILoggableRequest, ICacheRemoverRequest, ITransactionalRequest, IIntervalRequest
{
    public string Name { get; set; } = default!;
    public CountryPhoneCode CountryPhoneCode { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string Relation { get; set; } = default!;
    public Guid EmployeeId { get; set; }
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_EmergencyContacts";
    public string? CacheKey => null;
    public int Interval => 15;


    public class CreateEmergencyContactCommandHandler : IRequestHandler<CreateEmergencyContactCommand, CreateEmergencyContactResponse>
    {
        private readonly IEmergencyContactRepository emergencyContactRepository;
        private readonly EmergencyContactBusinessRules emergencyContactBusinessRules;
        private readonly EmployeeBusinessRules employeeBusinessRules;

        public CreateEmergencyContactCommandHandler(IEmergencyContactRepository emergencyContactRepository,
                                                    EmergencyContactBusinessRules emergencyContactBusinessRules,
                                                    EmployeeBusinessRules employeeBusinessRules)
        {
            this.emergencyContactRepository = emergencyContactRepository;
            this.emergencyContactBusinessRules = emergencyContactBusinessRules;
            this.employeeBusinessRules = employeeBusinessRules;
        }

        public async Task<CreateEmergencyContactResponse> Handle(CreateEmergencyContactCommand request, CancellationToken cancellationToken)
        {
            await emergencyContactBusinessRules.EnsurePhoneNumberIsUnique(request.PhoneNumber);
            await emergencyContactBusinessRules.EnsureEmployeeDoesNotHaveDuplicatePhoneNumber(request.EmployeeId, request.PhoneNumber);
            await emergencyContactBusinessRules.EnsureEmployeeHasNotExceededMaxEmergencyContacts(request.EmployeeId);
            emergencyContactBusinessRules.EnsureCountryPhoneCodeIsValid(request.CountryPhoneCode);
            await employeeBusinessRules.EnsureEmployeeExistsAsync(request.EmployeeId);

            EmergencyContactEntity? emergencyContact = request.Adapt<EmergencyContactEntity>();

            var result = await emergencyContactRepository.AddAsync(emergencyContact, cancellationToken);

            CreateEmergencyContactResponse response = result.Adapt<CreateEmergencyContactResponse>();

            return response;
        }
    }
}
