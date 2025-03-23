using Mapster;
using MapsterMapper;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetByAccountNumber;

public class GetBankDetailByAccountNumberQuery : IRequest<GetBankDetailByAccountNumberResponse>,
    ILoggableRequest, IIntervalRequest, ICachableRequest
{
    public string AccountNumber { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({AccountNumber})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_BankDetails";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;


    public class GetBankDetailByAccountNumberQueryHandler : IRequestHandler<GetBankDetailByAccountNumberQuery, GetBankDetailByAccountNumberResponse>
    {
        private readonly IBankDetailRepository bankDetailRepository;
        private readonly BankDetailBusinessRules bankDetailBusinessRules;

        public GetBankDetailByAccountNumberQueryHandler(IBankDetailRepository bankDetailRepository,
                                                        BankDetailBusinessRules bankDetailBusinessRules)
        {
            this.bankDetailRepository = bankDetailRepository;
            this.bankDetailBusinessRules = bankDetailBusinessRules;
        }

        public async Task<GetBankDetailByAccountNumberResponse> Handle(GetBankDetailByAccountNumberQuery request, CancellationToken cancellationToken)
        {
            BankDetailEntity? bankDetail = await bankDetailRepository.GetAsync(predicate: x => x.AccountNumber == request.AccountNumber,
                                                                               withDeleted: false,
                                                                               cancellationToken: cancellationToken);

            bankDetailBusinessRules.EnsureBankDetailExists(bankDetail);

            GetBankDetailByAccountNumberResponse response = bankDetail.Adapt<GetBankDetailByAccountNumberResponse>();

            return response;
        }
    }
}
