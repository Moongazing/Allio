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

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetListByBenefit;

public class GetListBenefitByNameQuery : IRequest<PaginatedResponse<GetListBenefitByNameResponse>>,
    ILoggableRequest, IIntervalRequest, ICachableRequest

{
    public string BenefitName { get; set; } = default!;
    public PageRequest PageRequest { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({PageRequest.PageIndex}-{PageRequest.PageSize}-{BenefitName.ToLower()})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_Benefits";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;



    public class GetListBenefitByNameQueryHandler : IRequestHandler<GetListBenefitByNameQuery, PaginatedResponse<GetListBenefitByNameResponse>>
    {
        private readonly IBenefitRepository benefitRepository;

        public GetListBenefitByNameQueryHandler(IBenefitRepository benefitRepository)
        {
            this.benefitRepository = benefitRepository;
        }

        public async Task<PaginatedResponse<GetListBenefitByNameResponse>> Handle(GetListBenefitByNameQuery request, CancellationToken cancellationToken)
        {
            IPaginate<BenefitEntity>? benefits = await benefitRepository.GetListAsync(predicate: x => x.BenefitName.ToLower() == request.BenefitName.ToLower(),
                                                                                      index: request.PageRequest.PageIndex,
                                                                                      size: request.PageRequest.PageSize,
                                                                                      withDeleted: false,
                                                                                      cancellationToken: cancellationToken);

            PaginatedResponse<GetListBenefitByNameResponse> response = benefits.Adapt<PaginatedResponse<GetListBenefitByNameResponse>>();

            return response;
        }
    }
}
