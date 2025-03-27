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

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetListByEmployeesApproachingBenefitLimit;

public class GetListBenefitByApproachingLimitQuery : IRequest<PaginatedResponse<GetListBenefitByApproachingLimitResponse>>,
ILoggableRequest, IIntervalRequest, ICachableRequest

{
    public decimal Threshold { get; set; } = default!;
    public PageRequest PageRequest { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({PageRequest.PageIndex}-{PageRequest.PageSize}-{Threshold})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_Benefits";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;


    public class GetListBenefitByApproachingLimitQueryHandler : IRequestHandler<GetListBenefitByApproachingLimitQuery, PaginatedResponse<GetListBenefitByApproachingLimitResponse>>
    {

        private readonly IBenefitRepository benefitRepository;

        public GetListBenefitByApproachingLimitQueryHandler(IBenefitRepository benefitRepository)
        {
            this.benefitRepository = benefitRepository;
        }

        public async Task<PaginatedResponse<GetListBenefitByApproachingLimitResponse>> Handle(GetListBenefitByApproachingLimitQuery request, CancellationToken cancellationToken)
        {
            IPaginate<BenefitLimitApproachingDto>? benefits = await benefitRepository.GetEmployeesWithHighBenefitUsageAsync(
                threshold: request.Threshold,
                pageIndex: request.PageRequest.PageIndex,
                pageSize: request.PageRequest.PageSize,
                cancellationToken: cancellationToken);

            PaginatedResponse<GetListBenefitByApproachingLimitResponse> response = benefits.Adapt<PaginatedResponse<GetListBenefitByApproachingLimitResponse>>();

            return response;
        }
    }
}
