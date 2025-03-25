using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetList;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Requests;
using Moongazing.Kernel.Application.Responses;
using Moongazing.Kernel.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetList;

public class GetListBenefitQuery : IRequest<PaginatedResponse<GetListBenefitResponse>>,
    ILoggableRequest, IIntervalRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({PageRequest.PageIndex}-{PageRequest.PageSize})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_Benefits";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;


    public class GetListBenefitQueryHandler : IRequestHandler<GetListBenefitQuery, PaginatedResponse<GetListBenefitResponse>>
    {
        private readonly IBenefitRepository benefitRepository;

        public GetListBenefitQueryHandler(IBenefitRepository benefitRepository)
        {
            this.benefitRepository = benefitRepository;
        }

        public async Task<PaginatedResponse<GetListBenefitResponse>> Handle(GetListBenefitQuery request, CancellationToken cancellationToken)
        {
            IPaginate<BenefitEntity>? benefits = await benefitRepository.GetListAsync(index: request.PageRequest.PageIndex,
                                                                                              size: request.PageRequest.PageSize,
                                                                                              withDeleted: false,
                                                                                              cancellationToken: cancellationToken);

            PaginatedResponse<GetListBenefitResponse> response = benefits.Adapt<PaginatedResponse<GetListBenefitResponse>>();

            return response;
        }
    }
}
