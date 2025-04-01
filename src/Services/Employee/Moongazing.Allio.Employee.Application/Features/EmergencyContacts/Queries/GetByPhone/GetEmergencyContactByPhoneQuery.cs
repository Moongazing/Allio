using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;

namespace Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Queries.GetByPhone;

public class GetEmergencyContactByPhoneQuery : IRequest<GetEmergencyContactByPhoneResponse>, ILoggableRequest, IIntervalRequest, ICachableRequest
{
    public CountryPhoneCode PhoneCode { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({(int)PhoneCode} {PhoneNumber})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_EmergencyContacts";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;

    public class GetEmergencyContactByPhoneQueryHandler : IRequestHandler<GetEmergencyContactByPhoneQuery, GetEmergencyContactByPhoneResponse>
    {
        private readonly IEmergencyContactRepository emergencyContactRepository;
        private readonly EmergencyContactBusinessRules emergencyContactBusinessRules;

        public GetEmergencyContactByPhoneQueryHandler(IEmergencyContactRepository emergencyContactRepository, EmergencyContactBusinessRules emergencyContactBusinessRules)
        {
            this.emergencyContactRepository = emergencyContactRepository;
            this.emergencyContactBusinessRules = emergencyContactBusinessRules;
        }

        public async Task<GetEmergencyContactByPhoneResponse> Handle(GetEmergencyContactByPhoneQuery request, CancellationToken cancellationToken)
        {
            EmergencyContactEntity? emergencyContact = await emergencyContactRepository.GetAsync(
                predicate: x => x.CountryPhoneCode == request.PhoneCode && x.PhoneNumber == request.PhoneNumber,
                cancellationToken: cancellationToken);

            emergencyContactBusinessRules.EnsureEmergencyContanctExists(emergencyContact);

            GetEmergencyContactByPhoneResponse response = emergencyContact!.Adapt<GetEmergencyContactByPhoneResponse>();
            return response;
        }
    }
}
