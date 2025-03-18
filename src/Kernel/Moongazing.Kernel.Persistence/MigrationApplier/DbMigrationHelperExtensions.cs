using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Moongazing.Kernel.Persistence.MigrationApplier;

public static class DbMigrationHelperExtensions
{
    public static DatabaseFacade EnsureDbApplied(this DatabaseFacade databaseFacade)
    {
        if (!databaseFacade.CanConnect())
        {
            databaseFacade.Migrate();
        }
        else
        {
            var pendingMigrations = databaseFacade.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                databaseFacade.Migrate();
            }
        }

        return databaseFacade;
    }
}
