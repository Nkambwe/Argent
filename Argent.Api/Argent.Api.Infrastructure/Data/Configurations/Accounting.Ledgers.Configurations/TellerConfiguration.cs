using Argent.Api.Domain.Entities.Banking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class TellerConfiguration : IEntityTypeConfiguration<Teller> {
        public void Configure(EntityTypeBuilder<Teller> builder) {
            builder.ToTable("bnk_teller");
            builder.Property(c => c.TellerCode).IsRequired().HasMaxLength(10);
            builder.Property(c => c.MaximumLimit).HasColumnType("decimal(18,2)");
            builder.Property(c => c.MinimumLimit).HasColumnType("decimal(18,2)");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasIndex(c => c.TellerCode).HasDatabaseName("ix_teller_code");

            builder.HasOne(c => c.AppUser)
                .WithMany()
                .HasForeignKey(c => c.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.TellerLedgerAccounts)
                .WithOne(l => l.Teller)
                .HasForeignKey(c => c.TellerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
