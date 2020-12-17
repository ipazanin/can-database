using System.Threading;
using System.Threading.Tasks;
using CanDatabase.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CanDatabase.Persistence.Database
{
    /// <summary>
    /// ICanDatabaseContext
    /// </summary>
    public interface ICanDatabaseContext
    {
        #region Properties
        public DbSet<CanDb> CanDbs { get; }
        public DbSet<Message> Messages { get; }
        public DbSet<Signal> Signals { get; }
        public DbSet<NetworkNode> NetworkNodes { get; }

        public DatabaseFacade Database { get; }
        #endregion

        #region Methods
        public DbSet<T> Set<T>() where T : class;
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        #endregion
    }
}
