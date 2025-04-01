using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;

namespace Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Queries.GetById;

public class GetEmergencyContactByIdQuery : IRequest<GetEmergencyContactByIdResponse>, ILoggableRequest, IIntervalRequest, ICachableRequest
{
    public Guid Id { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({Id})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_EmergencyContacts";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;

    public class GetEmergencyContactByIdQueryHandler : IRequestHandler<GetEmergencyContactByIdQuery, GetEmergencyContactByIdResponse>
    {
        private readonly IEmergencyContactRepository emergencyContactRepository;
        private readonly EmergencyContactBusinessRules emergencyContactBusinessRules;

        public GetEmergencyContactByIdQueryHandler(IEmergencyContactRepository emergencyContactRepository, EmergencyContactBusinessRules emergencyContactBusinessRules)
        {
            this.emergencyContactRepository = emergencyContactRepository;
            this.emergencyContactBusinessRules = emergencyContactBusinessRules;
        }

        public async Task<GetEmergencyContactByIdResponse> Handle(GetEmergencyContactByIdQuery request, CancellationToken cancellationToken)
        {
            EmergencyContactEntity? emergencyContact = await emergencyContactRepository.GetAsync(predicate: x => x.Id == request.Id,
                                                                                               cancellationToken: cancellationToken);

            emergencyContactBusinessRules.EnsureEmergencyContanctExists(emergencyContact);

            GetEmergencyContactByIdResponse response = emergencyContact!.Adapt<GetEmergencyContactByIdResponse>();
            return response;
        }
    }
}


