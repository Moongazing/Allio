namespace Moongazing.Kernel.Application.Responses;

public interface IBulkResponse : IResponse
{
    public int InsertedCount { get; set; }
    public int UpdatedCount { get; set; }
    public int DeletedCount { get; set; }
}