using MediatR;
using Microsoft.AspNetCore.Http;
using Moongazing.Kernel.CrossCuttingConcerns.Logging;
using Moongazing.Kernel.CrossCuttingConcerns.Logging.Serilog;
using System.Text.Json;


namespace Moongazing.Kernel.Application.Pipelines.Logging;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>, ILoggableRequest
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ILogger logger;

    public LoggingBehavior(IHttpContextAccessor httpContextAccessor, ILogger logger)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        List<LogParameter> logParameters =
        [
            new LogParameter
            {
                Type = request.GetType().Name,
                Value = request
            }
        ];

        LogDetail logDetail = new()
        {
            MethodName = next.Method.Name,
            Parameters = logParameters,
            User = httpContextAccessor.HttpContext?.User.Identity?.Name ?? "?"
        };

        logger.Information(JsonSerializer.Serialize(logDetail));
        return await next();
    }
}
