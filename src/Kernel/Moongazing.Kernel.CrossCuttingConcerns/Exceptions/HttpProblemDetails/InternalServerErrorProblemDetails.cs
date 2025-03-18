using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moongazing.Kernel.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public class InternalServerErrorProblemDetails : ProblemDetails
{
    public InternalServerErrorProblemDetails(string detail)
    {
        Title = "InternalServerError";
        Detail = detail;
        Status = StatusCodes.Status500InternalServerError;
        Type = "http://allio.com/probs/internal";
    }
}