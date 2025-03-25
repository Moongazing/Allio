using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Constants;
using Moongazing.Allio.Employee.Application.Features.Benefits.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetById;

public class GetBenefitByIdQuery : IRequest<GetBenefitByIdResponse>, ILoggableRequest, IIntervalRequest, ICachableRequest
{
    public Guid Id { get; set; }
    public PageRequest PageRequest { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({Id})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_Benefits";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;


    public class GetBenefitByIdQueryHandler : IRequestHandler<GetBenefitByIdQuery, GetBenefitByIdResponse>
    {
        private readonly IBenefitRepository benefitRepository;
        private readonly BenefitBusinessRules benefitBusinessRules;

        public GetBenefitByIdQueryHandler(IBenefitRepository benefitRepository,
                                          BenefitBusinessRules benefitBusinessRules)
        {
            this.benefitRepository = benefitRepository;
            this.benefitBusinessRules = benefitBusinessRules;
        }

        public async Task<GetBenefitByIdResponse> Handle(GetBenefitByIdQuery request, CancellationToken cancellationToken)
        {
            BenefitEntity? benefit = await benefitRepository.GetAsync(
                predicate: x => x.Id == request.Id,
                withDeleted: false,
                cancellationToken: cancellationToken);

            benefitBusinessRules.EnsureBenefitExists(benefit);

            GetBenefitByIdResponse response = benefit.Adapt<GetBenefitByIdResponse>();

            return response;


        }
    }
}
