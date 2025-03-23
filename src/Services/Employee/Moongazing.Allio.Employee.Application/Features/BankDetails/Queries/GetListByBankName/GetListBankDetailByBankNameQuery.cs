using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
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

namespace Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetListByBankName;

public class GetListBankDetailByBankNameQuery : IRequest<PaginatedResponse<GetListBankDetailByBankNameResponse>>,
    ILoggableRequest, IIntervalRequest, ICachableRequest
{
    public string BankName { get; set; } = default!;
    public PageRequest PageRequest { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({PageRequest.PageIndex}-{PageRequest.PageSize}-{BankName})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_BankDetails";
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;


    public class GetListBankDetailByBankNameQueryHandler : IRequestHandler<GetListBankDetailByBankNameQuery, PaginatedResponse<GetListBankDetailByBankNameResponse>>
    {
        private readonly IBankDetailRepository bankDetailRepository;
        public GetListBankDetailByBankNameQueryHandler(IBankDetailRepository bankDetailRepository)
        {
            this.bankDetailRepository = bankDetailRepository;
        }
        public async Task<PaginatedResponse<GetListBankDetailByBankNameResponse>> Handle(GetListBankDetailByBankNameQuery request, CancellationToken cancellationToken)
        {
            IPaginate<BankDetailEntity>? bankDetails = await bankDetailRepository.GetListAsync(
                predicate: x => x.BankName.Equals(request.BankName, StringComparison.CurrentCultureIgnoreCase),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                withDeleted: false,
                cancellationToken: cancellationToken);

            PaginatedResponse<GetListBankDetailByBankNameResponse> response = bankDetails.Adapt<PaginatedResponse<GetListBankDetailByBankNameResponse>>();

            return response;
        }
    }
}
