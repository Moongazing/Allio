using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.Benefits.DataTransferObjects;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Requests;
using Moongazing.Kernel.Application.Responses;
using Moongazing.Kernel.Persistence.Paging;

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetListByEmployeesApproachingBenefitCount;

public class GetListBenefitByApproachingCountQuery : IRequest<PaginatedResponse<GetListBenefitByApproachingCountResponse>>,
        ILoggableRequest, IIntervalRequest, ICachableRequest

{
    public int Threshold { get; set; } = default!;
    public PageRequest PageRequest { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({PageRequest.PageIndex}-{PageRequest.PageSize}-{Threshold})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_Benefits";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;

    public class GetListBenefitByApproachingBenefitCountQueryHandler : IRequestHandler<GetListBenefitByApproachingCountQuery, PaginatedResponse<GetListBenefitByApproachingCountResponse>>
    {

        private readonly IBenefitRepository benefitRepository;

        public GetListBenefitByApproachingBenefitCountQueryHandler(IBenefitRepository benefitRepository)
        {
            this.benefitRepository = benefitRepository;
        }

        public async Task<PaginatedResponse<GetListBenefitByApproachingCountResponse>> Handle(GetListBenefitByApproachingCountQuery request, CancellationToken cancellationToken)
        {
            IPaginate<BenefitCountApproachingDto>? benefits = await benefitRepository.GetEmployeesApproachingBenefitCountLimitAsync(
                threshold: request.Threshold,
                pageIndex: request.PageRequest.PageIndex,
                pageSize: request.PageRequest.PageSize,
                cancellationToken: cancellationToken);

            PaginatedResponse<GetListBenefitByApproachingCountResponse> response = benefits.Adapt<PaginatedResponse<GetListBenefitByApproachingCountResponse>>();

            return response;
        }
    }
}



