using Mapster;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Constants;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;

namespace Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetByEmployeeId;

public class GetBankDetailByEmployeeIdQuery : IRequest<GetBankDetailByEmployeeIdResponse>,
    ILoggableRequest, IIntervalRequest, ICachableRequest

{
    public Guid EmployeeId { get; set; } = default!;
    public string CacheKey => $"{GetType().Name}({EmployeeId})";
    public bool BypassCache { get; }
    public string? CacheGroupKey => BankDetailConstants.BankDetailCacheKey;
    public TimeSpan? SlidingExpiration { get; }
    public int Interval => 15;


    public class GetBankDetailByEmployeeIdQueryHandler:IRequestHandler<GetBankDetailByEmployeeIdQuery, GetBankDetailByEmployeeIdResponse>
    {
        private readonly IBankDetailRepository bankDetailRepository;
        private readonly BankDetailBusinessRules bankDetailBusinessRules;
        public GetBankDetailByEmployeeIdQueryHandler(IBankDetailRepository bankDetailRepository,
                                                    BankDetailBusinessRules bankDetailBusinessRules)
        {
            this.bankDetailRepository = bankDetailRepository;
            this.bankDetailBusinessRules = bankDetailBusinessRules;
        }
        public async Task<GetBankDetailByEmployeeIdResponse> Handle(GetBankDetailByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            BankDetailEntity? bankDetail = await bankDetailRepository.GetAsync(predicate: x => x.EmployeeId == request.EmployeeId,
                                                                               withDeleted: false,
                                                                               cancellationToken: cancellationToken);
            bankDetailBusinessRules.EnsureBankDetailExists(bankDetail);

            GetBankDetailByEmployeeIdResponse response = bankDetail!.Adapt<GetBankDetailByEmployeeIdResponse>();
            return response;
        }
    }
}
