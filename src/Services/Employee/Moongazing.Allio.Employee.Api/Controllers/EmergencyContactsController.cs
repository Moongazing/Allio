using Microsoft.AspNetCore.Mvc;
using Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Delete;
using Moongazing.Allio.Employee.Application.Features.Departments.Commands.Update;
using Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Commands.Create;
using Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Commands.Delete;
using Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Commands.Update;
using Moongazing.Kernel.Shared.Controller;

namespace Moongazing.Allio.Employee.Api.Controllers;
[Route("api/allio/emergencycontacts")]
[ApiController]
public sealed class EmergencyContactsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateEmergencyContactCommand createEmergencyContactCommand)
    {
        CreateEmergencyContactResponse result = await Sender.Send(createEmergencyContactCommand).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteEmergencyContactCommand deleteEmergencyContactCommand = new() { Id = id };
        DeleteEmergencyContactResponse result = await Sender.Send(deleteEmergencyContactCommand).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateEmergencyContactCommand updateEmergencyContactCommand)
    {
        UpdateEmergencyContactResponse result = await Sender.Send(updateEmergencyContactCommand).ConfigureAwait(false);
        return Ok(result);
    }
}
