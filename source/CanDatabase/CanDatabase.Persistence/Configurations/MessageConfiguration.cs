using CanDatabase.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CanDatabase.Persistence.Configurations
{
    /// <summary>
    /// MessageConfiguration
    /// </summary>
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(message => message.Name)
                .HasMaxLength(Message.NameMaxLength);

            builder.HasOne(message => message.CanDb)
                .WithMany(canDb => canDb!.Messages)
                .HasForeignKey(message => message.CanDbId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(message => message.Signals)
                .WithOne(signal => signal.Message!)
                .HasForeignKey(signal => signal.MessageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
