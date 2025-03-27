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

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetListByDynamic;

public class GetListBenefitByDynamicQuery : IRequest<PaginatedResponse<GetListBenefitByDynamicResponse>>,
      ILoggableRequest, IIntervalRequest
{
    public DynamicQuery DynamicQuery { get; set; } = default!;
    public PageRequest PageRequest { get; set; } = default!;
    public int Interval => 15;

    public class GetListBenefitByDynamicQueryHandler : IRequestHandler<GetListBenefitByDynamicQuery, PaginatedResponse<GetListBenefitByDynamicResponse>>
    {
        private readonly IBenefitRepository benefitRepository;

        public GetListBenefitByDynamicQueryHandler(IBenefitRepository benefitRepository)
        {
            this.benefitRepository = benefitRepository;
        }

        public async Task<PaginatedResponse<GetListBenefitByDynamicResponse>> Handle(GetListBenefitByDynamicQuery request, CancellationToken cancellationToken)
        {
            IPaginate<BenefitEntity>? benefits = await benefitRepository.GetListByDynamicAsync(
                dynamic: request.DynamicQuery,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                withDeleted: false,
                cancellationToken: cancellationToken);

            PaginatedResponse<GetListBenefitByDynamicResponse> response = benefits.Adapt<PaginatedResponse<GetListBenefitByDynamicResponse>>();

            return response;
        }
    }
}