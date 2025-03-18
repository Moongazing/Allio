using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moongazing.Kernel.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public class NotFoundProblemDetails : ProblemDetails
{
    public NotFoundProblemDetails(string detail)
    {
        Title = "NotFound";
        Detail = detail;
        Status = StatusCodes.Status404NotFound;
        Type = "http://allio.com/probs/notfound";
    }
}

