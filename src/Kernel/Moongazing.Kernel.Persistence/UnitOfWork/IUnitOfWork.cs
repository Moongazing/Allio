using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moongazing.Kernel.Persistence.Repositories;

public interface IUnitOfWork : IAsyncDisposable
{

    IAsyncRepository<TEntity, TEntityId> GetRepository<TEntity, TEntityId>()
        where TEntity : Entity<TEntityId>;


    IAsyncRepository<TEntity, TEntityId> GetRepository<TEntity, TEntityId, TContext>()
        where TEntity : Entity<TEntityId>
        where TContext : DbContext;

    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
