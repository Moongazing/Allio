using Microsoft.AspNetCore.Mvc;
using Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetList;
using Moongazing.Allio.Employee.Application.Features.Benefits.Queries.GetListByDynamic;
using Moongazing.Allio.Employee.Application.Features.Departments.Commands.Create;
using Moongazing.Allio.Employee.Application.Features.Departments.Commands.Delete;
using Moongazing.Allio.Employee.Application.Features.Departments.Commands.Update;
using Moongazing.Allio.Employee.Application.Features.Departments.Queries.GetByDepartmentManager;
using Moongazing.Allio.Employee.Application.Features.Departments.Queries.GetById;
using Moongazing.Allio.Employee.Application.Features.Departments.Queries.GetByName;
using Moongazing.Allio.Employee.Application.Features.Departments.Queries.GetList;
using Moongazing.Allio.Employee.Application.Features.Departments.Queries.GetListByDynamic;
using Moongazing.Kernel.Application.Requests;
using Moongazing.Kernel.Application.Responses;
using Moongazing.Kernel.Persistence.Dynamic;
using Moongazing.Kernel.Shared.Controller;

namespace Moongazing.Allio.Employee.Api.Controllers;

[Route("api/allio/departments")]
[ApiController]
public sealed class DepartmentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateDepartmentCommand createDepartmentCommand)
    {
        CreateDepartmentResponse result = await Sender.Send(createDepartmentCommand).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteDepartmentCommand deleteBenefitCommand = new() { Id = id };
        DeleteDepartmentResponse result = await Sender.Send(deleteBenefitCommand).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateDepartmentCommand updateDepartmentCommand)
    {
        UpdateDepartmentResponse result = await Sender.Send(updateDepartmentCommand).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpGet("by-department-manager/{managerId}")]
    public async Task<IActionResult> GetByDepartmentManager([FromRoute] Guid managerId)
    {
        GetDepartmentByManagerIdQuery getDepartmentsByDepartmentManagerQuery = new() { DepartmentManagerId = managerId };
        GetDepartmentByManagerIdResponse result = await Sender.Send(getDepartmentsByDepartmentManagerQuery).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpGet("by-id/{id}")]
    public async Task<IActionResult> GetByDepartmentId([FromRoute] Guid id)
    {
        GetDepartmentByIdQuery getDepartmentByIdQuery = new() { Id = id };
        GetDepartmentByIdResponse result = await Sender.Send(getDepartmentByIdQuery).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpGet("by-name/{name}")]
    public async Task<IActionResult> GetDepartmentByName([FromRoute] string name)
    {
        GetDepartmentByNameQuery getDepartmentByNameQuery = new() { Name = name };
        GetDepartmentByNameResponse result = await Sender.Send(getDepartmentByNameQuery).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListDepartmentQuery getListBenefitQuery = new() { PageRequest = pageRequest };
        PaginatedResponse<GetListDepartmentResponse> result = await Sender.Send(getListBenefitQuery).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpPost("filter")]
    public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery dynamicQuery)
    {
        GetListDepartmentByDynamicQuery getListBenefitByDynamicQuery = new() { PageRequest = pageRequest, DynamicQuery = dynamicQuery! };
        PaginatedResponse<GetListDepartmentByDynamicResponse> result = await Sender.Send(getListBenefitByDynamicQuery).ConfigureAwait(false);
        return Ok(result);
    }
}
