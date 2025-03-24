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
    [HttpGet("by-iban/{iban}")]
    public async Task<IActionResult> GetByIBAN([FromRoute] string iban)
    {
        GetBankDetailByIBANQuery getBankDetailByIBANQuery = new() { IBAN = iban };
        GetBankDetailByIBANResponse result = await Sender.Send(getBankDetailByIBANQuery).ConfigureAwait(false);
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
    [HttpGet("by-bank-name/{bankName}")]
    public async Task<IActionResult> GetListByBankName([FromQuery] PageRequest pageRequest, string bankName)
    {
        GetListBankDetailByBankNameQuery getListBankDetailByBankNameQuery = new() { PageRequest = pageRequest, BankName = bankName };
        PaginatedResponse<GetListBankDetailByBankNameResponse> result = await Sender.Send(getListBankDetailByBankNameQuery).ConfigureAwait(false);
        return Ok(result);
    }
    [HttpGet("by-currency/{currency}")]
    public async Task<IActionResult> GetListByCurrency([FromQuery] PageRequest pageRequest, Currency currency)
    {
        GetListBankDetailByCurrencyQuery getListBankDetailByBankNameQuery = new() { PageRequest = pageRequest, Currency = currency };
        PaginatedResponse<GetListBankDetailByCurrencyResponse> result = await Sender.Send(getListBankDetailByBankNameQuery).ConfigureAwait(false);
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