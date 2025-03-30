using Microsoft.AspNetCore.Mvc;
using Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Create;
using Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Delete;
using Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Update;
using Moongazing.Allio.Employee.Application.Features.Departments.Commands.Create;
using Moongazing.Allio.Employee.Application.Features.Departments.Commands.Delete;
using Moongazing.Allio.Employee.Application.Features.Departments.Commands.Update;
using Moongazing.Kernel.Shared.Controller;

namespace Moongazing.Allio.Employee.Api.Controllers;

[Route("api/allio/departments")]
[ApiController]
public sealed class DepartmentsController:BaseController
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
}
