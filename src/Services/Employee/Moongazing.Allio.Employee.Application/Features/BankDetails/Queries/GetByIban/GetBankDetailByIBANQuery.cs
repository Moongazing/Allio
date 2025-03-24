using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Constants;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetByIban;

public class GetBankDetailByIBANQuery : IRequest<GetBankDetailByIBANResponse>,
ILoggableRequest, IIntervalRequest, ICachableRequest
{

    public string IBAN { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({IBAN})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => BankDetailConstants.BankDetailCacheKey;
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;


    public class GetBankDetailByIBANQueryHandler:IRequestHandler<GetBankDetailByIBANQuery,GetBankDetailByIBANResponse>
    {
        private readonly IBankDetailRepository bankDetailRepository;
        private readonly BankDetailBusinessRules bankDetailBusinessRules;
        public GetBankDetailByIBANQueryHandler(IBankDetailRepository bankDetailRepository,
                                               BankDetailBusinessRules bankDetailBusinessRules)
        {
            this.bankDetailRepository = bankDetailRepository;
            this.bankDetailBusinessRules = bankDetailBusinessRules;
        }
        public async Task<GetBankDetailByIBANResponse> Handle(GetBankDetailByIBANQuery request, CancellationToken cancellationToken)
        {
            BankDetailEntity? bankDetail = await bankDetailRepository.GetAsync(predicate: x => x.IBAN == request.IBAN,
                                                                               withDeleted: false,
                                                                               cancellationToken: cancellationToken);
            bankDetailBusinessRules.EnsureBankDetailExists(bankDetail);

            GetBankDetailByIBANResponse response = bankDetail!.Adapt<GetBankDetailByIBANResponse>();
            return response;
        }
    }

}
