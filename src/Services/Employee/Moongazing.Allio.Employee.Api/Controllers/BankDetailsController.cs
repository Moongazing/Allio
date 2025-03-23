using Microsoft.AspNetCore.Mvc;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Commands.Create;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Commands.Delete;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Commands.Update;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetByAccountNumber;
using Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetByEmployeeId;
using Moongazing.Kernel.Shared.Controller;

namespace Moongazing.Allio.Employee.Api.Controllers;

[Route("api/allio/bankdetails")]
[ApiController]
public sealed class BankDetailsController : BaseController
{

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBankDetailCommand createBankDetailCommand)
    {
        CreateBankDetailResponse result = await Sender.Send(createBankDetailCommand).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteBankDetailCommand deleteBankDetailCommand = new() { Id = id };
        DeleteBankDetailResponse result = await Sender.Send(deleteBankDetailCommand).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBankDetailCommand updateBankDetailCommand)
    {
        UpdateBankDetailResponse result = await Sender.Send(updateBankDetailCommand).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpGet("by-account-number/{accountNumber}")]
    public async Task<IActionResult> GetByAccountNumber([FromRoute] string accountNumber)
    {
        GetBankDetailByAccountNumberQuery getBankDetailByAccountNumberQuery = new() { AccountNumber = accountNumber };
        GetBankDetailByAccountNumberResponse result = await Sender.Send(getBankDetailByAccountNumberQuery).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpGet("by-employee-id/{employeeId}")]
    public async Task<IActionResult> GetByEmployeeId([FromRoute] Guid employeeId)
    {
        GetBankDetailByEmployeeIdQuery getBankDetailByEmployeeIdQuery = new() { EmployeeId = employeeId };
        GetBankDetailByEmployeeIdResponse result = await Sender.Send(getBankDetailByEmployeeIdQuery).ConfigureAwait(false);
        return Ok(result);
    }
}