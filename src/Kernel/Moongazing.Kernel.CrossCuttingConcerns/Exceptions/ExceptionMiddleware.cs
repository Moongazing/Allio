using Microsoft.AspNetCore.Http;
using Moongazing.Kernel.CrossCuttingConcerns.Exceptions.Handlers;
using Moongazing.Kernel.CrossCuttingConcerns.Logging;
using Moongazing.Kernel.CrossCuttingConcerns.Logging.Serilog;
using System.Net.Mime;
using System.Text.Json;

namespace Moongazing.Kernel.CrossCuttingConcerns.Exceptions;

public class ExceptionMiddleware
{
    private readonly IHttpContextAccessor contextAccessor;
    private readonly HttpExceptionHandler httpExceptionHandler = new();
    private readonly ILogger loggerService;
    private readonly RequestDelegate next;

    public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor contextAccessor, ILogger loggerService)
    {
        this.contextAccessor = contextAccessor;
        this.loggerService = loggerService;
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await LogException(context, exception);
            await HandleExceptionAsync(context.Response, exception);
        }
    }

    protected virtual Task HandleExceptionAsync(HttpResponse response, dynamic exception)
    {
        response.ContentType = MediaTypeNames.Application.Json;

        httpExceptionHandler.Response = response;

        return httpExceptionHandler.HandleException(exception);
    }

    protected virtual Task LogException(HttpContext context, Exception exception)
    {
        List<LogParameter> logParameters = [new LogParameter { Type = context.GetType().Name, Value = exception.ToString() }];

        LogDetail logDetail =
            new()
            {
                MethodName = next.Method.Name,
                Parameters = logParameters,
                User = contextAccessor.HttpContext?.User.Identity?.Name ?? "?"
            };

        loggerService.Information(JsonSerializer.Serialize(logDetail));
        return Task.CompletedTask;
    }
}