using MediatR;
using Microsoft.Extensions.Logging;
using System.Transactions;

namespace Moongazing.Kernel.Application.Pipelines.Transaction;

public class TransactionScopeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ITransactionalRequest
{
    private readonly ILogger<TransactionScopeBehavior<TRequest, TResponse>> logger;

    public TransactionScopeBehavior(ILogger<TransactionScopeBehavior<TRequest, TResponse>> logger)
    {
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        try
        {
            logger.LogInformation($"Transaction started for {typeof(TRequest).Name}");

            TResponse response = await next();

            transactionScope.Complete();
            logger.LogInformation($"Transaction committed for {typeof(TRequest).Name}");

            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Transaction rolled back for {typeof(TRequest).Name} due to an error.");
            throw;
        }
    }
}