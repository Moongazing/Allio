using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Allio.Employee.Domain.Enums;
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

namespace Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetListByCurrency;

public class GetListBankDetailByCurrencyQuery : IRequest<PaginatedResponse<GetListBankDetailByCurrencyResponse>>,
  ILoggableRequest, IIntervalRequest, ICachableRequest

{
    public Currency Currency { get; set; }
    public PageRequest PageRequest { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({PageRequest.PageIndex}-{PageRequest.PageSize}-{Currency})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_BankDetails";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;


    public class GetListBankDetailByCurrencyQueryHandler : IRequestHandler<GetListBankDetailByCurrencyQuery, PaginatedResponse<GetListBankDetailByCurrencyResponse>>
    {
        private readonly IBankDetailRepository bankDetailRepository;
        public GetListBankDetailByCurrencyQueryHandler(IBankDetailRepository bankDetailRepository)
        {
            this.bankDetailRepository = bankDetailRepository;
        }
        public async Task<PaginatedResponse<GetListBankDetailByCurrencyResponse>> Handle(GetListBankDetailByCurrencyQuery request, CancellationToken cancellationToken)
        {
            IPaginate<BankDetailEntity>? bankDetails = await bankDetailRepository.GetListAsync(
                predicate: x => x.Currency.Equals(request.Currency),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                withDeleted: false,
                cancellationToken: cancellationToken);

            PaginatedResponse<GetListBankDetailByCurrencyResponse> response = bankDetails.Adapt<PaginatedResponse<GetListBankDetailByCurrencyResponse>>();
            return response;
        }
    }
}
