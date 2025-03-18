using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moongazing.Kernel.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public class AuthorizationProblemDetails : ProblemDetails
{
    public AuthorizationProblemDetails(string detail)
    {
        Title = "AuthorizationProblem";
        Detail = detail;
        Status = StatusCodes.Status401Unauthorized;
        Type = "http://allio.com/probs/business";
    }
}
