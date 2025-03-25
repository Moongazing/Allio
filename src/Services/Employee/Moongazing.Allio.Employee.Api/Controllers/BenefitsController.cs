using Microsoft.AspNetCore.Mvc;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Commands.Update;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetById;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetList;
using Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Create;
using Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Delete;
using Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Update;
using Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetById;
using Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetList;
using Moongazing.Kernel.Application.Requests;
using Moongazing.Kernel.Application.Responses;
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
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBenefitCommand updateBenefitCommand)
    {
        UpdateBenefitResponse result = await Sender.Send(updateBenefitCommand).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetBenefitByIdQuery getBenefitByIdQuery = new() { Id = id };
        GetBenefitByIdResponse result = await Sender.Send(getBenefitByIdQuery).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBenefitQuery getListBenefitQuery = new() { PageRequest = pageRequest };
        PaginatedResponse<GetListBenefitResponse> result = await Sender.Send(getListBenefitQuery).ConfigureAwait(false);
        return Ok(result);
    }

}