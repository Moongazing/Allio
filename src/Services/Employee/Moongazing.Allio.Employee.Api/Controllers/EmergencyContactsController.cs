using Microsoft.AspNetCore.Mvc;
using Moongazing.Allio.Employee.Application.Features.EmergencyContacts.Commands.Create;
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
}
