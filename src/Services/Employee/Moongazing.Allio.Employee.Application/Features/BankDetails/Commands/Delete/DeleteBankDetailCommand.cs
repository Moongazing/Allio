using Mapster;
using MapsterMapper;
using MediatR;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Rules;
using Moongazing.Allio.Employee.Application.Repositories;
using Moongazing.Allio.Employee.Domain.Entities;
using Moongazing.Kernel.Application.Pipelines.Caching;
using Moongazing.Kernel.Application.Pipelines.Logging;
using Moongazing.Kernel.Application.Pipelines.Performance;
using Moongazing.Kernel.Application.Pipelines.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongazing.Allio.Employee.Application.Features.BankDetails.Commands.Delete;

public class DeleteBankDetailCommand : IRequest<DeleteBankDetailResponse>,
    ILoggableRequest, ICacheRemoverRequest, ITransactionalRequest, IIntervalRequest
{
    public Guid Id { get; set; }
    public bool BypassCache { get; }
    public string? CacheGroupKey => "Employee_BankDetails";
    public string? CacheKey => null;
    public int Interval => 15;


    public class DeleteBankDetailCommandHandler : IRequestHandler<DeleteBankDetailCommand, DeleteBankDetailResponse>
    {
        private readonly IBankDetailRepository bankDetailRepository;
        private readonly BankDetailBusinessRules bankDetailBusinessRules;
        public DeleteBankDetailCommandHandler(IBankDetailRepository bankDetailRepository,
                                              BankDetailBusinessRules bankDetailBusinessRules)
        {
            this.bankDetailRepository = bankDetailRepository;
            this.bankDetailBusinessRules = bankDetailBusinessRules;
        }
        public async Task<DeleteBankDetailResponse> Handle(DeleteBankDetailCommand request, CancellationToken cancellationToken)
        {

            BankDetailEntity? bankDetail = await bankDetailRepository.GetAsync(predicate: x => x.Id == request.Id,
                                                                               cancellationToken: cancellationToken);

            bankDetailBusinessRules.EnsureBankDetailExists(bankDetail);

            await bankDetailRepository.DeleteAsync(bankDetail!, cancellationToken: cancellationToken, permanent: false);

            DeleteBankDetailResponse response = bankDetail.Adapt<DeleteBankDetailResponse>();

            return response;
        }
    }
}

