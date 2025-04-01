using Microsoft.AspNetCore.Mvc;
using Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Commands.Create;
using Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Commands.Delete;
using Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Commands.Update;
using Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Queries.GetById;
using Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Queries.GetByPhone;
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
    [HttpGet("by-id/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetEmergencyContactByIdQuery getEmergencyContactByIdQuery = new() { Id = id };
        GetEmergencyContactByIdResponse result = await Sender.Send(getEmergencyContactByIdQuery).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpGet("by-phone")]
    public async Task<IActionResult> GetByPhone([FromQuery] GetEmergencyContactByPhoneQuery query)
    {
        GetEmergencyContactByPhoneResponse result = await Sender.Send(query).ConfigureAwait(false);
        return Ok(result);
    }

}
