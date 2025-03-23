using Moongazing.Kernel.Application.Responses;

namespace Moongazing.Allio.Employee.Application.Features.BankDetails.Queries.GetListByCurrency;

public class GetListBankDetailByCurrencyResponse : IResponse
{
    public Guid Id { get; set; }
    public string BankName { get; set; } = default!;
    public string AccountNumber { get; set; } = default!;
    public string IBAN { get; set; } = default!;
    public string Currency { get; set; } = default!;
    public Guid EmployeeId { get; set; }
}