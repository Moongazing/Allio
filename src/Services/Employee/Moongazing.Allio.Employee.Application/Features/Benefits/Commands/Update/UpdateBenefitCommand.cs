using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.Benefits.Rules;
using Moongazing.Allio.Employee.Application.Features.Employees.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Pipelines.Transaction;

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Update;

public class UpdateBenefitCommand : IRequest<UpdateBenefitResponse>, 
    ILoggableRequest, ICacheRemoverRequest, IIntervalRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string BenefitName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Value { get; set; }
    public Guid EmployeeId { get; set; }
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_Benefits";
    public string? CacheKey => null;
    public int Interval => 15;


    public class UpdateBenefitCommandHandler : IRequestHandler<UpdateBenefitCommand, UpdateBenefitResponse>
    {
        private readonly IBenefitRepository benefitRepository;
        private readonly BenefitBusinessRules benefitBusinessRules;
        private readonly EmployeeBusinessRules employeeBusinessRules;
        public UpdateBenefitCommandHandler(IBenefitRepository benefitRepository,
                                           BenefitBusinessRules benefitBusinessRules,
                                           EmployeeBusinessRules employeeBusinessRules)
        {
            this.benefitRepository = benefitRepository;
            this.benefitBusinessRules = benefitBusinessRules;
            this.employeeBusinessRules = employeeBusinessRules;
        }
        public async Task<UpdateBenefitResponse> Handle(UpdateBenefitCommand request, CancellationToken cancellationToken)
        {
            await employeeBusinessRules.EnsureEmployeeExistsAsync(request.EmployeeId);
            await benefitBusinessRules.EnsureEmployeeDoesNotHaveBenefit(request.EmployeeId, request.BenefitName);
            await benefitBusinessRules.EnsureBenefitLimitNotExceeded(request.EmployeeId, request.Value);

            BenefitEntity? benefit = await benefitRepository.GetAsync(predicate: x => x.Id == request.Id,
                                                                      cancellationToken: cancellationToken);

            benefitBusinessRules.EnsureBenefitExists(benefit);

            benefit = request.Adapt<BenefitEntity>();

            var result = await benefitRepository.UpdateAsync(benefit, cancellationToken);

            UpdateBenefitResponse response = result.Adapt<UpdateBenefitResponse>();

            return response;
        }
    }
}
