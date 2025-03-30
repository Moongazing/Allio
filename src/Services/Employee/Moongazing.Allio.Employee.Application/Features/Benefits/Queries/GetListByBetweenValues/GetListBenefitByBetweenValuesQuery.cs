using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.Benefits.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Requests;
using Moongazing.Kernel.Application.Responses;
using Moongazing.Kernel.Persistence.Paging;

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetListByBetweenValues;

public class GetListBenefitByBetweenValuesQuery : IRequest<PaginatedResponse<GetListBenefitByBetweenValuesResponse>>,
        ILoggableRequest, IIntervalRequest, ICachableRequest

{
    public decimal MinValue { get; set; } = default!;
    public decimal MaxValue { get; set; } = default!;
    public bool IncludeValues { get; set; } = default!;
    public PageRequest PageRequest { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({PageRequest.PageIndex}-{PageRequest.PageSize}-({MinValue}-{MaxValue}))";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_Benefits";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;



    public class GetListBenefitByBetweenValuesQueryHandler : IRequestHandler<GetListBenefitByBetweenValuesQuery, PaginatedResponse<GetListBenefitByBetweenValuesResponse>>

    {

        private readonly IBenefitRepository benefitRepository;
        private readonly BenefitBusinessRules benefitBusinessRules;

        public GetListBenefitByBetweenValuesQueryHandler(IBenefitRepository benefitRepository, BenefitBusinessRules benefitBusinessRules)
        {
            this.benefitRepository = benefitRepository;
            this.benefitBusinessRules = benefitBusinessRules;
        }

        public async Task<PaginatedResponse<GetListBenefitByBetweenValuesResponse>> Handle(GetListBenefitByBetweenValuesQuery request, CancellationToken cancellationToken)
        {

            benefitBusinessRules.EnsureBenefitValueRangeIsValid(request.MinValue, request.MaxValue);

            IPaginate<BenefitEntity>? benefits;

            if (request.IncludeValues)
            {
                benefits = await benefitRepository.GetListAsync(
                    predicate: x => x.Value >= request.MinValue && x.Value <= request.MaxValue,
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    withDeleted: false,
                    cancellationToken: cancellationToken);
            }
            else
            {
                benefits = await benefitRepository.GetListAsync(
                    predicate: x => x.Value > request.MinValue && x.Value < request.MaxValue,
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    withDeleted: false,
                    cancellationToken: cancellationToken);
            }


            PaginatedResponse<GetListBenefitByBetweenValuesResponse> response = benefits.Adapt<PaginatedResponse<GetListBenefitByBetweenValuesResponse>>();

            return response;
        }
    }
}

