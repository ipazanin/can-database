using CanDatabase.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CanDatabase.Persistence.Configurations
{
    /// <summary>
    /// MessageConfiguration
    /// </summary>
    public class NetworkNodeConfiguration : IEntityTypeConfiguration<NetworkNode>
    {
        public void Configure(EntityTypeBuilder<NetworkNode> builder)
        {
            builder.Property(networkNode => networkNode.Name)
                .HasMaxLength(NetworkNode.NameMaxLength);

            builder.HasOne(networkNode => networkNode.CanDb)
                .WithMany(canDb => canDb!.NetworkNodes)
                .HasForeignKey(networkNode => networkNode.CanDbId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
