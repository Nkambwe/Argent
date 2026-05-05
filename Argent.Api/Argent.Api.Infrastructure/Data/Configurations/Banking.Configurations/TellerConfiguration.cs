using Argent.Api.Domain.Entities;
using Argent.Api.Domain.Entities.Banking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Banking.Configurations {
    internal class TellerConfiguration : IEntityTypeConfiguration<Teller> {
        public void Configure(EntityTypeBuilder<Teller> builder) {
            builder.ToTable("tellers");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.CreatedBy).HasMaxLength(100);
            builder.Property(b => b.UpdatedBy).HasMaxLength(100);
            builder.Property(b => b.DeletedBy).HasMaxLength(100);
            
            builder.HasOne(b => b.AppUser)
                .WithMany(h => h.Tellers)
                .HasForeignKey(h => h.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.AppUser)
                .WithMany(h => h.Tellers)
                .HasForeignKey(h => h.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
