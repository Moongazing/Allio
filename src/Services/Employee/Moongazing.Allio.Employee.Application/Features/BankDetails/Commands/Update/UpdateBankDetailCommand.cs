using Mapster;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongazing.Allio.Employee.Application.Features.BankDetails.Commands.Update;

public class UpdateBankDetailCommand:IRequest<UpdateBankDetailResponse>,
    ILoggableRequest, ICacheRemoverRequest, ITransactionalRequest, IIntervalRequest
{

    public Guid Id { get; set; }
    public string BankName { get; set; } = default!;
    public string AccountNumber { get; set; } = default!;
    public string IBAN { get; set; } = default!;
    public string Currency { get; set; } = default!;
    public Guid EmployeeId { get; set; }
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_BankDetails";
    public string? CacheKey => null;
    public int Interval => 15;



    public class UpdateBankDetailCommandHandler : IRequestHandler<UpdateBankDetailCommand, UpdateBankDetailResponse>
    {
        private readonly IBankDetailRepository bankDetailRepository;
        private readonly BankDetailBusinessRules bankDetailBusinessRules;
        private readonly EmployeeBusinessRules employeeBusinessRules;
        public UpdateBankDetailCommandHandler(IBankDetailRepository bankDetailRepository,
                                              BankDetailBusinessRules bankDetailBusinessRules,
                                              EmployeeBusinessRules employeeBusinessRules)
        {
            this.bankDetailRepository = bankDetailRepository;
            this.bankDetailBusinessRules = bankDetailBusinessRules;
            this.employeeBusinessRules = employeeBusinessRules;
        }
        public async Task<UpdateBankDetailResponse> Handle(UpdateBankDetailCommand request, CancellationToken cancellationToken)
        {
            await employeeBusinessRules.EnsureEmployeeExistsAsync(request.EmployeeId);
            await bankDetailBusinessRules.EnsureUniqueIBAN(request.IBAN);
            BankDetailEntity? bankDetail = await bankDetailRepository.GetAsync(predicate: x => x.Id == request.Id,
                                                                               cancellationToken: cancellationToken);
            bankDetailBusinessRules.EnsureBankDetailExists(bankDetail);
            bankDetail = request.Adapt(bankDetail);
            bankDetail!.Currency = Enum.Parse<Currency>(request.Currency);
            var result = await bankDetailRepository.UpdateAsync(bankDetail, cancellationToken);
            UpdateBankDetailResponse response = result.Adapt<UpdateBankDetailResponse>();
            return response;
        }
    }
}
