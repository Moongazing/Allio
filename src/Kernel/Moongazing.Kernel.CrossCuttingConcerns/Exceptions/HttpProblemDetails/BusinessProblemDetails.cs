using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Moongazing.Kernel.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public class BusinessProblemDetails : ProblemDetails
{
    public BusinessProblemDetails(string detail)
    {
        Title = "RuleViolation";
        Detail = detail;
        Status = StatusCodes.Status400BadRequest;
        Type = "http://allio.com/probs/business";
    }
}