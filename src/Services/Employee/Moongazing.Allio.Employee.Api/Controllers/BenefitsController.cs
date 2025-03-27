using Microsoft.AspNetCore.Mvc;
using Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Create;
using Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Delete;
using Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Update;
using Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetById;
using Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetList;
using Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetListByBenefit;
using Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetListByEmployee;
using Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetListByEmployeesApproachingBenefitCount;
using Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetListByEmployeesApproachingBenefitLimit;
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
    [HttpGet("by-benefit/{benefit}")]
    public async Task<IActionResult> GetListByBenefit([FromQuery] PageRequest pageRequest, [FromRoute] string benefit)
    {
        GetListBenefitByNameQuery getListByBenefitQuery = new() { PageRequest = pageRequest, BenefitName = benefit };
        PaginatedResponse<GetListBenefitByNameResponse> result = await Sender.Send(getListByBenefitQuery).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpGet("by-employee/{employeeId}")]
    public async Task<IActionResult> GetListByEmployee([FromQuery] PageRequest pageRequest, [FromRoute] Guid employeeId)
    {
        GetListBenefitByEmployeeIdQuery getListBenefitByEmployeeIdQuery = new() { PageRequest = pageRequest, EmployeeId = employeeId };
        PaginatedResponse<GetListBenefitByEmployeeIdResponse> result = await Sender.Send(getListBenefitByEmployeeIdQuery).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpGet("approaching-count")]
    public async Task<IActionResult> GetListByApproachingCount([FromQuery] PageRequest pageRequest, [FromQuery] int threshold)
    {
        GetListBenefitByApproachingCountQuery query = new()
        {
            PageRequest = pageRequest,
            Threshold = threshold
        };
        PaginatedResponse<GetListBenefitByApproachingCountResponse> result = await Sender.Send(query).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpGet("approaching-limit")]
    public async Task<IActionResult> GetListByApproachingLimit([FromQuery] PageRequest pageRequest, [FromQuery] decimal threshold)
    {
        GetListBenefitByApproachingLimitQuery query = new()
        {
            PageRequest = pageRequest,
            Threshold = threshold
        };

        PaginatedResponse<GetListBenefitByApproachingLimitResponse> result = await Sender.Send(query).ConfigureAwait(false);
        return Ok(result);
    }



}