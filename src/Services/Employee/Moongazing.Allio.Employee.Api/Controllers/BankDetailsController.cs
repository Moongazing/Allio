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
using Moongazing.Allio.Employee.Domain.Enums;
using Moongazing.Kernel.Application.Requests;
using Moongazing.Kernel.Application.Responses;
using Moongazing.Kernel.Persistence.Dynamic;
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
    [HttpGet("by-account-number")]
    public async Task<IActionResult> GetByAccountNumber([FromQuery] string accountNumber)
    {
        var query = new GetBankDetailByAccountNumberQuery { AccountNumber = accountNumber };
        var result = await Sender.Send(query).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet("by-employee-id")]
    public async Task<IActionResult> GetByEmployeeId([FromQuery] Guid employeeId)
    {
        var query = new GetBankDetailByEmployeeIdQuery { EmployeeId = employeeId };
        var result = await Sender.Send(query).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet("by-iban")]
    public async Task<IActionResult> GetByIBAN([FromQuery] string iban)
    {
        var query = new GetBankDetailByIBANQuery { IBAN = iban };
        var result = await Sender.Send(query).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet("by-bank-name")]
    public async Task<IActionResult> GetListByBankName([FromQuery] PageRequest pageRequest, [FromQuery] string bankName)
    {
        var query = new GetListBankDetailByBankNameQuery { PageRequest = pageRequest, BankName = bankName };
        var result = await Sender.Send(query).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet("by-currency")]
    public async Task<IActionResult> GetListByCurrency([FromQuery] PageRequest pageRequest, [FromQuery] Currency currency)
    {
        var query = new GetListBankDetailByCurrencyQuery { PageRequest = pageRequest, Currency = currency };
        var result = await Sender.Send(query).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetBankDetailByIdQuery getBankDetailByIdQuery = new() { Id = id };
        GetBankDetailByIdResponse result = await Sender.Send(getBankDetailByIdQuery).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBankDetailQuery getListBankDetailQuery = new() { PageRequest = pageRequest };
        PaginatedResponse<GetListBankDetailResponse> result = await Sender.Send(getListBankDetailQuery).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpPost("filter")]
    public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery dynamicQuery)
    {
        GetListBankDetailByDynamicQuery getListBankDetailByDynamicQuery = new() { PageRequest = pageRequest, DynamicQuery = dynamicQuery! };
        PaginatedResponse<GetListBankDetailByDynamicResponse> result = await Sender.Send(getListBankDetailByDynamicQuery).ConfigureAwait(false);
        return Ok(result);
    }
}