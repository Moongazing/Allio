using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moongazing.Kernel.Persistence.Repositories;

namespace Moongazing.Kernel.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly Dictionary<Type, DbContext> contexts;
    private readonly Dictionary<Type, object> repositories;
    private IDbContextTransaction? currentTransaction;
    private DbContext? transactionContext;
    private bool disposed;

    public UnitOfWork(IEnumerable<DbContext> contexts)
    {
        this.contexts = new Dictionary<Type, DbContext>();
        foreach (var context in contexts)
        {
            this.contexts[context.GetType()] = context;
        }
        repositories = new Dictionary<Type, object>();
    }

    public IAsyncRepository<TEntity, TEntityId> GetRepository<TEntity, TEntityId>()
        where TEntity : Entity<TEntityId>
    {
        return GetRepository<TEntity, TEntityId, DbContext>();
    }

    public IAsyncRepository<TEntity, TEntityId> GetRepository<TEntity, TEntityId, TContext>()
        where TEntity : Entity<TEntityId>
        where TContext : DbContext
    {
        var repositoryKey = typeof(EfRepositoryBase<TEntity, TEntityId, TContext>);

        if (!repositories.TryGetValue(repositoryKey, out var repository))
        {
            if (!contexts.TryGetValue(typeof(TContext), out var context))
            {
                throw new InvalidOperationException($"No context of type {typeof(TContext).Name} has been registered with this UnitOfWork.");
            }

            repository = Activator.CreateInstance(
                typeof(EfRepositoryBase<TEntity, TEntityId, TContext>),
                (TContext)context);

            if (repository == null)
            {
                throw new InvalidOperationException($"Failed to create repository for {typeof(TEntity).Name} with context {typeof(TContext).Name}");
            }

            repositories.Add(repositoryKey, repository);
        }

        return (IAsyncRepository<TEntity, TEntityId>)repository;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (currentTransaction != null)
        {
            return currentTransaction;
        }

        transactionContext = contexts.Values.First();
        currentTransaction = await transactionContext.Database.BeginTransactionAsync(cancellationToken);

        foreach (var context in contexts.Values.Where(c => c != transactionContext))
        {
            await context.Database.UseTransactionAsync(currentTransaction.GetDbTransaction(), cancellationToken);
        }

        return currentTransaction;
    }
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (currentTransaction == null)
        {
            throw new InvalidOperationException("No active transaction to commit.");
        }

        try
        {

            foreach (var context in contexts.Values)
            {
                await context.SaveChangesAsync(cancellationToken);
            }

            await currentTransaction.CommitAsync(cancellationToken);
        }
        finally
        {
            await currentTransaction.DisposeAsync();
            currentTransaction = null;
            transactionContext = null;
        }
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (currentTransaction == null)
        {
            throw new InvalidOperationException("No active transaction to rollback.");
        }

        try
        {
            await currentTransaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            await currentTransaction.DisposeAsync();
            currentTransaction = null;
            transactionContext = null;
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int totalChanges = 0;
        foreach (var context in contexts.Values)
        {
            totalChanges += await context.SaveChangesAsync(cancellationToken);
        }
        return totalChanges;
    }


    protected virtual async Task DisposeAsync(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {

                if (currentTransaction != null)
                {
                    await currentTransaction.DisposeAsync();
                    currentTransaction = null;
                }


                foreach (var context in contexts.Values)
                {
                    await context.DisposeAsync();
                }

                contexts.Clear();
                repositories.Clear();
            }

            disposed = true;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
