using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetListByBenefit;
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
using System.Xml.Linq;

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetListByEmployee;

public class GetListBenefitByEmployeeIdQuery : IRequest<PaginatedResponse<GetListBenefitByEmployeeIdResponse>>,
        ILoggableRequest, IIntervalRequest, ICachableRequest
{
    public Guid EmployeeId { get; set; } = default!;
    public PageRequest PageRequest { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({PageRequest.PageIndex}-{PageRequest.PageSize}-{EmployeeId})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_Benefits";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;

    public class GetListBenefitByEmployeeIdQueryHandler : IRequestHandler<GetListBenefitByEmployeeIdQuery, PaginatedResponse<GetListBenefitByEmployeeIdResponse>>
    {
        private readonly IBenefitRepository benefitRepository;

        public GetListBenefitByEmployeeIdQueryHandler(IBenefitRepository benefitRepository)
        {
            this.benefitRepository = benefitRepository;
        }

        public async Task<PaginatedResponse<GetListBenefitByEmployeeIdResponse>> Handle(GetListBenefitByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            IPaginate<BenefitEntity>? benefits = await benefitRepository.GetListAsync(predicate: x => x.EmployeeId == request.EmployeeId,
                                                                                      index: request.PageRequest.PageIndex,
                                                                                      size: request.PageRequest.PageSize,
                                                                                      withDeleted: false,
                                                                                      cancellationToken: cancellationToken);

            PaginatedResponse<GetListBenefitByEmployeeIdResponse> response = benefits.Adapt<PaginatedResponse<GetListBenefitByEmployeeIdResponse>>();

            return response;
        }
    }

}
