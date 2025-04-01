using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Requests;
using Moongazing.Kernel.Application.Responses;
using Moongazing.Kernel.Persistence.Dynamic;
using Moongazing.Kernel.Persistence.Paging;

namespace Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Queries.GetListByDynamic;

public class GetListEmergencyContactByDynamicQuery : IRequest<PaginatedResponse<GetListEmergencyContactByDynamicResponse>>,
    ILoggableRequest, IIntervalRequest
{
    public DynamicQuery DynamicQuery { get; set; } = default!;
    public PageRequest PageRequest { get; set; } = default!;
    public int Interval => 15;

    public class GetListEmergencyContactByDynamicQueryHandler : IRequestHandler<GetListEmergencyContactByDynamicQuery, PaginatedResponse<GetListEmergencyContactByDynamicResponse>>
    {
        private readonly IEmergencyContactRepository emergencyContactRepository;

        public GetListEmergencyContactByDynamicQueryHandler(IEmergencyContactRepository emergencyContactRepository)
        {
            this.emergencyContactRepository = emergencyContactRepository;
        }

        public async Task<PaginatedResponse<GetListEmergencyContactByDynamicResponse>> Handle(GetListEmergencyContactByDynamicQuery request, CancellationToken cancellationToken)
        {
            IPaginate<EmergencyContactEntity>? contacts = await emergencyContactRepository.GetListByDynamicAsync(
                dynamic: request.DynamicQuery,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                withDeleted: false,
                cancellationToken: cancellationToken);

            PaginatedResponse<GetListEmergencyContactByDynamicResponse> response =
                contacts.Adapt<PaginatedResponse<GetListEmergencyContactByDynamicResponse>>();

            return response;
        }
    }
}
