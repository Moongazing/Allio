using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.Departments.Commands.Update;
using Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Commands.Create;
using Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Rules;
using Moongazing.Allio.Employee.Application.Features.Employees.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Pipelines.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Commands.Update;

public class UpdateEmergencyContactCommand : IRequest<UpdateEmergencyContactResponse>,
    ILoggableRequest, ICacheRemoverRequest, ITransactionalRequest, IIntervalRequest

{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public CountryPhoneCode CountryPhoneCode { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string Relation { get; set; } = default!;
    public Guid EmployeeId { get; set; }
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_EmergencyContacts";
    public string? CacheKey => null;
    public int Interval => 15;


    public class UpdateEmergencyContactCommandHandler : IRequestHandler<UpdateEmergencyContactCommand, UpdateEmergencyContactResponse>
    {
        private readonly IEmergencyContactRepository emergencyContactRepository;
        private readonly EmergencyContactBusinessRules emergencyContactBusinessRules;
        private readonly EmployeeBusinessRules employeeBusinessRules;

        public UpdateEmergencyContactCommandHandler(IEmergencyContactRepository emergencyContactRepository, EmergencyContactBusinessRules emergencyContactBusinessRules, EmployeeBusinessRules employeeBusinessRules)
        {
            this.emergencyContactRepository = emergencyContactRepository;
            this.emergencyContactBusinessRules = emergencyContactBusinessRules;
            this.employeeBusinessRules = employeeBusinessRules;
        }

        public async Task<UpdateEmergencyContactResponse> Handle(UpdateEmergencyContactCommand request, CancellationToken cancellationToken)
        {
            EmergencyContactEntity? emergencyContact = await emergencyContactRepository.GetAsync(predicate: x => x.Id == request.Id,
                                                                                                 cancellationToken: cancellationToken);

            emergencyContactBusinessRules.EnsureEmergencyContanctExists(emergencyContact);
            await emergencyContactBusinessRules.EnsurePhoneNumberIsUnique(request.PhoneNumber);
            await emergencyContactBusinessRules.EnsureEmployeeDoesNotHaveDuplicatePhoneNumber(request.EmployeeId, request.PhoneNumber);
            await emergencyContactBusinessRules.EnsureEmployeeHasNotExceededMaxEmergencyContacts(request.EmployeeId);
            emergencyContactBusinessRules.EnsureCountryPhoneCodeIsValid(request.CountryPhoneCode);
            await employeeBusinessRules.EnsureEmployeeExistsAsync(request.EmployeeId);

            emergencyContact = request.Adapt<EmergencyContactEntity>();

            var result = await emergencyContactRepository.UpdateAsync(emergencyContact, cancellationToken);

            UpdateEmergencyContactResponse response = result.Adapt<UpdateEmergencyContactResponse>();

            return response;
        }
    }
}
