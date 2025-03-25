using Microsoft.AspNetCore.Mvc;
using Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Create;
using Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Delete;
using Moongazing.Kernel.Shared.Controller;

namespace Moongazing.Allio.Employee.Api.Controllers;

[Route("api/allio/benefits")]
[ApiController]
public sealed class BenefitsController : BaseController
{

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBenefitCommand createBenefitCommand)
    {
        CreateBenefitResponse result = await Sender.Send(createBenefitCommand).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteBenefitCommand deleteBenefitCommand = new() { Id = id };
        DeleteBenefitResponse result = await Sender.Send(deleteBenefitCommand).ConfigureAwait(false);
        return Ok(result);
    }

}