using Moongazing.Kernel.CrossCuttingConcerns.Exceptions.Types;

namespace Moongazing.Kernel.CrossCuttingConcerns.Exceptions.Handlers;

public abstract class ExceptionHandler
{
    public abstract Task HandleException(BusinessException businessException);
    public abstract Task HandleException(ValidationException validationException);
    public abstract Task HandleException(AuthorizationException authorizationException);
    public abstract Task HandleException(NotFoundException notFoundException);
    public abstract Task HandleException(Exception exception);
}
