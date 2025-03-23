using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetById;

public class GetBankDetailByIdQuery : IRequest<GetBankDetailByIdResponse>,
    ILoggableRequest, IIntervalRequest, ICachableRequest

{
    public Guid Id { get; set; }
    public string CacheKey => $"{GetType().Name}({Id})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_BankDetails";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;



    public class GetBankDetailByIdQueryHandler : IRequestHandler<GetBankDetailByIdQuery, GetBankDetailByIdResponse>
    {
        private readonly IBankDetailRepository bankDetailRepository;
        private readonly BankDetailBusinessRules bankDetailBusinessRules;
        public GetBankDetailByIdQueryHandler(IBankDetailRepository bankDetailRepository,
                                             BankDetailBusinessRules bankDetailBusinessRules)
        {
            this.bankDetailRepository = bankDetailRepository;
            this.bankDetailBusinessRules = bankDetailBusinessRules;
        }
        public async Task<GetBankDetailByIdResponse> Handle(GetBankDetailByIdQuery request, CancellationToken cancellationToken)
        {
            BankDetailEntity? bankDetail = await bankDetailRepository.GetAsync(predicate: x => x.Id == request.Id,
                                                                               withDeleted: false,
                                                                               cancellationToken: cancellationToken);
            bankDetailBusinessRules.EnsureBankDetailExists(bankDetail);
            
            GetBankDetailByIdResponse response = bankDetail!.Adapt<GetBankDetailByIdResponse>();

            return response;
        }
    }
}

