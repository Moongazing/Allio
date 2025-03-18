using MediatR;
using Moongazing.Kernel.CrossCuttingConcerns.Logging;
using Moongazing.Kernel.CrossCuttingConcerns.Logging.Serilog;
using System.Diagnostics;
using System.Text.Json;

namespace Moongazing.Kernel.Application.Pipelines.Performance;

public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IIntervalRequest
{
    private readonly Stopwatch stopwatch;
    private readonly ILogger logger;

    public PerformanceBehavior(Stopwatch stopwatch, ILogger logger)
    {
        this.stopwatch = stopwatch;
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = request.GetType().Name;

        TResponse response;

        try
        {
            stopwatch.Start();
            response = await next();
        }
        finally
        {
            stopwatch.Stop();

            if (stopwatch.Elapsed.TotalSeconds > request.Interval)
            {
                string message = $"Performance -> {requestName} took {stopwatch.Elapsed.TotalSeconds} seconds";

                LogDetail logDetail = new()
                {
                    MethodName = next.Method.Name,
                    Parameters = new List<LogParameter>
                    {
                        new() {
                            Type = request.GetType().Name,
                            Value = request
                        }
                    },
                    User = "PerformanceCheck"
                };

                logger.Information($"{message} | Details: {JsonSerializer.Serialize(logDetail)}");
            }

            stopwatch.Reset();
        }

        return response;
    }
}
