using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Requests;
using Moongazing.Kernel.Application.Responses;
using Moongazing.Kernel.Persistence.Paging;

namespace Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Queries.GetList;

public class GetListEmergencyContactQuery:IRequest<PaginatedResponse<GetListEmergencyContactResponse>>, 
    ILoggableRequest, IIntervalRequest, ICachableRequest

{
    public PageRequest PageRequest { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({PageRequest.PageIndex}-{PageRequest.PageSize})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_EmergencyContacts";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;

    public class GetListEmergencyContactQueryHandler : IRequestHandler<GetListEmergencyContactQuery, PaginatedResponse<GetListEmergencyContactResponse>>
    {
        private readonly IEmergencyContactRepository emergencyContactRepository;

        public GetListEmergencyContactQueryHandler(IEmergencyContactRepository emergencyContactRepository)
        {
            this.emergencyContactRepository = emergencyContactRepository;
        }
        public async Task<PaginatedResponse<GetListEmergencyContactResponse>> Handle(GetListEmergencyContactQuery request, CancellationToken cancellationToken)
        {
            IPaginate<EmergencyContactEntity>? emergencyContact = await emergencyContactRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                withDeleted: false,
                cancellationToken: cancellationToken);

            PaginatedResponse<GetListEmergencyContactResponse> response = emergencyContact.Adapt<PaginatedResponse<GetListEmergencyContactResponse>>();

            return response;
        }
    }
}
