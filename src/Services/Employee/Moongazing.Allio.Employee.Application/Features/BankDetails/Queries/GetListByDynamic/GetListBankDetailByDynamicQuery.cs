using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Requests;
using Moongazing.Kernel.Application.Responses;
using Moongazing.Kernel.Persistence.Dynamic;
using Moongazing.Kernel.Persistence.Paging;

namespace Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetListByDynamic;

public class GetListBankDetailByDynamicQuery : IRequest<PaginatedResponse<GetListBankDetailByDynamicResponse>>,
      ILoggableRequest, IIntervalRequest
{
    public DynamicQuery DynamicQuery { get; set; } = default!;
    public PageRequest PageRequest { get; set; } = default!;
    public int Interval => 15;

    public class GetListBankDetailByDynamicQueryHandler : IRequestHandler<GetListBankDetailByDynamicQuery, PaginatedResponse<GetListBankDetailByDynamicResponse>>
    {
        private readonly IBankDetailRepository bankDetailRepository;

        public GetListBankDetailByDynamicQueryHandler(IBankDetailRepository bankDetailRepository)
        {
            this.bankDetailRepository = bankDetailRepository;
        }

        public async Task<PaginatedResponse<GetListBankDetailByDynamicResponse>> Handle(GetListBankDetailByDynamicQuery request, CancellationToken cancellationToken)
        {
            IPaginate<BankDetailEntity>? bankDetail = await bankDetailRepository.GetListByDynamicAsync(
                dynamic: request.DynamicQuery,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                withDeleted: false,
                cancellationToken: cancellationToken);


            PaginatedResponse<GetListBankDetailByDynamicResponse> response = bankDetail.Adapt<PaginatedResponse<GetListBankDetailByDynamicResponse>>();

            return response;
        }
    }
}
