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

namespace Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Queries.GetListByEmployee;

public class GetListEmergencyContactByEmployeeIdQuery : IRequest<PaginatedResponse<GetListEmergencyContactByEmployeeIdResponse>>,
    ILoggableRequest, IIntervalRequest, ICachableRequest
{
    public Guid EmployeeId { get; set; }
    public PageRequest PageRequest { get; set; } = default!;

    public string CacheKey => $"{GetType().Name}({EmployeeId}-{PageRequest.PageIndex}-{PageRequest.PageSize})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_EmergencyContacts";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;

    public class GetListEmergencyContactByEmployeeIdQueryHandler : IRequestHandler<GetListEmergencyContactByEmployeeIdQuery, PaginatedResponse<GetListEmergencyContactByEmployeeIdResponse>>
    {
        private readonly IEmergencyContactRepository emergencyContactRepository;

        public GetListEmergencyContactByEmployeeIdQueryHandler(IEmergencyContactRepository emergencyContactRepository)
        {
            this.emergencyContactRepository = emergencyContactRepository;
        }

        public async Task<PaginatedResponse<GetListEmergencyContactByEmployeeIdResponse>> Handle(GetListEmergencyContactByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            IPaginate<EmergencyContactEntity> emergencyContacts = await emergencyContactRepository.GetListAsync(
                predicate: x => x.EmployeeId == request.EmployeeId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                withDeleted: false,
                cancellationToken: cancellationToken);

            PaginatedResponse<GetListEmergencyContactByEmployeeIdResponse> response =
                emergencyContacts.Adapt<PaginatedResponse<GetListEmergencyContactByEmployeeIdResponse>>();

            return response;
        }
    }
}
