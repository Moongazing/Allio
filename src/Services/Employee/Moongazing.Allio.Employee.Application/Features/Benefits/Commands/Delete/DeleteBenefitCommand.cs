using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.Benefits.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Pipelines.Transaction;

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Delete;

public class DeleteBenefitCommand : IRequest<DeleteBenefitResponse>, ILoggableRequest, ICacheRemoverRequest, IIntervalRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_Benefits";
    public string? CacheKey => null;
    public int Interval => 15;

    public class DeleteBenefitCommandHandler : IRequestHandler<DeleteBenefitCommand, DeleteBenefitResponse>
    {
        private readonly IBenefitRepository benefitRepository;
        private readonly BenefitBusinessRules benefitBusinessRules;

        public DeleteBenefitCommandHandler(IBenefitRepository benefitRepository,
                                           BenefitBusinessRules benefitBusinessRules)
        {
            this.benefitRepository = benefitRepository;
            this.benefitBusinessRules = benefitBusinessRules;
        }

        public async Task<DeleteBenefitResponse> Handle(DeleteBenefitCommand request, CancellationToken cancellationToken)
        {
            BenefitEntity? benefit = await benefitRepository.GetAsync(predicate: x => x.Id == request.Id,
                                                                      cancellationToken: cancellationToken);

            benefitBusinessRules.EnsureBenefitExists(benefit);

            await benefitRepository.DeleteAsync(benefit!, cancellationToken: cancellationToken, permanent: false);

            DeleteBenefitResponse response = benefit.Adapt<DeleteBenefitResponse>();

            return response;
        }
    }

}
