using CanDatabase.Domain.Models;
using CanDatabase.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CanDatabase.Persistence.Database
{
    /// <summary>
    /// CanDatabaseContext
    /// </summary>
    public class CanDatabaseContext : DbContext, ICanDatabaseContext
    {
        #region Properties
        public DbSet<CanDb> CanDbs { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Signal> Signals { get; set; } = null!;
        public DbSet<NetworkNode> NetworkNodes { get; set; } = null!;
        #endregion

        #region Constructors
        public CanDatabaseContext(DbContextOptions<CanDatabaseContext> options)
            : base(options)
        {
        }
        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations(
                configurationsAssembly: typeof(CanDatabaseContext).Assembly
            );
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        #endregion
    }
}
