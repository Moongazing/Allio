using Moongazing.Kernel.Persistence.Paging;

namespace Moongazing.Kernel.Application.Responses;

public class PaginatedResponse<T> : PaginationMetadata
{
    public IList<T> Items { get; set; } = [];
}
