using CanDatabase.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CanDatabase.Persistence.Configurations
{
    /// <summary>
    /// SignalConfiguration
    /// </summary>
    public class SignalConfiguration : IEntityTypeConfiguration<Signal>
    {
        public void Configure(EntityTypeBuilder<Signal> builder)
        {
            builder.Property(signal => signal.Name)
                .HasMaxLength(Signal.NameMaxLength);

            builder.HasOne(signal => signal.Message)
                .WithMany(message => message!.Signals)
                .HasForeignKey(signal => signal.MessageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
