using CanDatabase.Infrastructure.Persistence.DesignTime;
using CanDatabase.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace CanDatabase.Persistence.DesignTime
{
    /// <summary>
    /// DesignTimeDatabaseContextFactory
    /// </summary>
    public class DesignTimeDatabaseContextFactory 
        : DesignTimeDatabaseContextFactoryBase<CanDatabaseContext>
    {
        protected override CanDatabaseContext CreateNewInstance(
            DbContextOptions<CanDatabaseContext> options
        )
        {
            return new CanDatabaseContext(options);
        }
    }
}
