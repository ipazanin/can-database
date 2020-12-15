using CanDatabase.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CanDatabase.Persistence.Configurations
{
    /// <summary>
    /// CanDbConfiguration
    /// </summary>
    public class CanDbConfiguration : IEntityTypeConfiguration<CanDb>
    {
        public void Configure(EntityTypeBuilder<CanDb> builder)
        {
            builder.HasMany(canDb => canDb.Messages)
                .WithOne(message => message.CanDb!)
                .HasForeignKey(message => message.CanDbId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(canDb => canDb.NetworkNodes)
                .WithOne(networkNode => networkNode.CanDb!)
                .HasForeignKey(networkNode => networkNode.CanDbId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
