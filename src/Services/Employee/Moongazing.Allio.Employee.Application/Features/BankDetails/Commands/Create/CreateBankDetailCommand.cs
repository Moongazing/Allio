using Mapster;
using MapsterMapper;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Rules;
using Moongazing.Allio.Employee.Application.Features.Employees.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Pipelines.Transaction;

namespace Moongazing.Allio.Employee.Application.Features.BankDetails.Commands.Create;

public class CreateBankDetailCommand : IRequest<CreateBankDetailResponse>,
    ILoggableRequest, ICacheRemoverRequest, ITransactionalRequest, IIntervalRequest
{
    public string BankName { get; set; } = default!;
    public string AccountNumber { get; set; } = default!;
    public string IBAN { get; set; } = default!;
    public string Currency { get; set; } = default!;
    public Guid EmployeeId { get; set; }
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_BankDetails";
    public string? CacheKey => null;
    public int Interval => 15;

    public class CreateBankDetailCommandHandler : IRequestHandler<CreateBankDetailCommand, CreateBankDetailResponse>
    {
        private readonly IBankDetailRepository bankDetailRepository;
        private readonly BankDetailBusinessRules bankDetailBusinessRules;
        private readonly EmployeeBusinessRules employeeBusinessRules;

        public CreateBankDetailCommandHandler(IBankDetailRepository bankDetailRepository,
                                              BankDetailBusinessRules bankDetailBusinessRules,
                                              EmployeeBusinessRules employeeBusinessRules)
        {
            this.bankDetailRepository = bankDetailRepository;
            this.bankDetailBusinessRules = bankDetailBusinessRules;
            this.employeeBusinessRules = employeeBusinessRules;
        }

        public async Task<CreateBankDetailResponse> Handle(CreateBankDetailCommand request, CancellationToken cancellationToken)
        {
            await employeeBusinessRules.EnsureEmployeeExistsAsync(request.EmployeeId);
            await bankDetailBusinessRules.EnsureUniqueIBAN(request.IBAN);

            BankDetailEntity? bankDetail = request.Adapt<BankDetailEntity>();

            bankDetail.Currency = Enum.Parse<Currency>(request.Currency);

            var result = await bankDetailRepository.AddAsync(bankDetail, cancellationToken);

            CreateBankDetailResponse response = result.Adapt<CreateBankDetailResponse>();

            return response;
        }
       
    }
}
