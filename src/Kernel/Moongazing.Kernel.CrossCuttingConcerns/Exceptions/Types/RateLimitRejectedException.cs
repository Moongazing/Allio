namespace Moongazing.Kernel.CrossCuttingConcerns.Exceptions.Types;

public class RateLimitRejectedException : Exception
{
    public RateLimitRejectedException() { }

    public RateLimitRejectedException(string? message) : base(message) { }

    public RateLimitRejectedException(string? message, Exception? innerException) : base(message, innerException) { }
}
