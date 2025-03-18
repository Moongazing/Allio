namespace Moongazing.Kernel.Application.Requests;

public class PageRequest
{
    public int PageIndex { get; set; } = default!;
    public int PageSize { get; set; } = default!;
}
