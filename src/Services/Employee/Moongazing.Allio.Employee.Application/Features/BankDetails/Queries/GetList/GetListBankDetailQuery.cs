using Mapster;
using MapsterMapper;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Constants;
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

namespace Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetList;

public class GetListBankDetailQuery : IRequest<PaginatedResponse<GetListBankDetailResponse>>,
        ILoggableRequest, IIntervalRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({PageRequest.PageIndex}-{PageRequest.PageSize})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => BankDetailConstants.BankDetailCacheKey;
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;

    public class GetListBankDetailQueryHandler : IRequestHandler<GetListBankDetailQuery, PaginatedResponse<GetListBankDetailResponse>>
    {
        private readonly IBankDetailRepository bankDetailRepository;

        public GetListBankDetailQueryHandler(IBankDetailRepository bankDetailRepository)
        {
            this.bankDetailRepository = bankDetailRepository;
        }

        public async Task<PaginatedResponse<GetListBankDetailResponse>> Handle(GetListBankDetailQuery request, CancellationToken cancellationToken)
        {
            IPaginate<BankDetailEntity>? bankDetails = await bankDetailRepository.GetListAsync(index: request.PageRequest.PageIndex,
                                                                                               size: request.PageRequest.PageSize,
                                                                                               withDeleted: false,
                                                                                               cancellationToken: cancellationToken);

            PaginatedResponse<GetListBankDetailResponse> response = bankDetails.Adapt<PaginatedResponse<GetListBankDetailResponse>>();

            return response;
        }
    }
}
