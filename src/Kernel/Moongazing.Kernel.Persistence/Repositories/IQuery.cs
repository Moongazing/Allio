namespace Moongazing.Kernel.Persistence.Repositories;

public interface IQuery<T>
{
    IQueryable<T> Query();
}
