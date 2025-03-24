using Microsoft.AspNetCore.Mvc;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Commands.Create;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Commands.Delete;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Commands.Update;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetByAccountNumber;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetByEmployeeId;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetByIban;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetById;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetList;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetListByBankName;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetListByCurrency;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetListByDynamic;
using Moongazing.Allio.Employee.Application.Features.Benefits.Commands.Create;
using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Application.Requests;
using Moongazing.Kernel.Application.Responses;
using Moongazing.Kernel.Persistence.Dynamic;
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
    
}