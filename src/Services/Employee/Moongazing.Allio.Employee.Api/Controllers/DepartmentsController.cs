using Microsoft.AspNetCore.Mvc;
using Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Create;
using Moongazing.Allio.Employee.Application.Features.Departments.Commands.Create;
using Moongazing.Kernel.Shared.Controller;

namespace Moongazing.Allio.Employee.Api.Controllers;

[Route("api/allio/departments")]
[ApiController]
public class DepartmentsController:BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateDepartmentCommand createDepartmentCommand)
    {
        CreateDepartmentResponse result = await Sender.Send(createDepartmentCommand).ConfigureAwait(false);
        return Ok(result);
    }
}
