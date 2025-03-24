using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Commands.Create;
using Moongazing.Allio.Employee.Application.Features.Benefits.Rules;
using Moongazing.Allio.Employee.Application.Features.Employees.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Pipelines.Transaction;

namespace Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Create;

public class CreateBenefitCommand : IRequest<CreateBenefitResponse>,
    ILoggableRequest, ICacheRemoverRequest, ITransactionalRequest, IIntervalRequest
{
    public string BenefitName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Value { get; set; }
    public Guid EmployeeId { get; set; }
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_Benefits";
    public string? CacheKey => null;
    public int Interval => 15;


    public class CreateBenefitCommandHandler : IRequestHandler<CreateBenefitCommand, CreateBenefitResponse>
    {
        private readonly IBenefitRepository benefitRepository;
        private readonly BenefitBusinessRules benefitBusinessRules;
        private readonly EmployeeBusinessRules employeeBusinessRules;

        public CreateBenefitCommandHandler(IBenefitRepository benefitRepository,
                                           BenefitBusinessRules benefitBusinessRules,
                                           EmployeeBusinessRules employeeBusinessRules)
        {
            this.benefitRepository = benefitRepository;
            this.benefitBusinessRules = benefitBusinessRules;
            this.employeeBusinessRules = employeeBusinessRules;
        }

        public async Task<CreateBenefitResponse> Handle(CreateBenefitCommand request, CancellationToken cancellationToken)
        {
            await employeeBusinessRules.EnsureEmployeeExistsAsync(request.EmployeeId);

            await benefitBusinessRules.EnsureEmployeeDoesNotHaveBenefit(request.EmployeeId, request.BenefitName);

            await benefitBusinessRules.EnsureBenefitLimitNotExceeded(request.EmployeeId, request.Value);

            BenefitEntity? benefit = request.Adapt<BenefitEntity>();

            var result = await benefitRepository.AddAsync(benefit, cancellationToken);

            CreateBenefitResponse response = result.Adapt<CreateBenefitResponse>();

            return response;

        }
    }
}
