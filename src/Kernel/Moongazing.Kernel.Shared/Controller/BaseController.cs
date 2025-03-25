using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Moongazing.Kernel.Shared.Controller;

public class BaseController : ControllerBase
{
    protected IMediator Sender =>
      _sender ??= HttpContext.RequestServices.GetService<IMediator>()
        ?? throw new InvalidOperationException("IMediator cannot be retrieved from request services.");

    private IMediator? _sender;

    protected string GetIpAddress()
    {
        return Request.Headers.TryGetValue("X-Forwarded-For", out Microsoft.Extensions.Primitives.StringValues value) ? value.ToString()
             : HttpContext.Connection.RemoteIpAddress?.MapToIPv4()
                                                      .ToString()
                 ?? throw new InvalidOperationException("IP address cannot be retrieved from request.");
    }

}