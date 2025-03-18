using Microsoft.EntityFrameworkCore;

namespace Moongazing.Kernel.Persistence.MigrationApplier;
public class DbMigrationApplierService<TDbContext> : IDbMigrationApplierService<TDbContext>
    where TDbContext : DbContext
{
    private readonly TDbContext context;
    public DbMigrationApplierService(TDbContext context)
    {
        this.context = context;
    }
    public void Initialize()
    {
        context.Database.EnsureDbApplied();
    }
}