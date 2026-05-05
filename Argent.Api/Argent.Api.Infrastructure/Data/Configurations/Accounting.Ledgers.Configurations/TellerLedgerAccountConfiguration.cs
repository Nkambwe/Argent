using Argent.Api.Domain.Entities.Banking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class TellerLedgerAccountConfiguration : IEntityTypeConfiguration<TellerLedgerAccount> {
        public void Configure(EntityTypeBuilder<TellerLedgerAccount> builder) {
            builder.ToTable("bnk_teller_acc");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasIndex(cv => new { cv.TellerId, cv.LegderAccountId }).IsUnique()
                .HasFilter("\"IsDeleted\" = false")
                .HasDatabaseName("ux_bnk_teller_ledger");

            builder.HasOne(cv => cv.Teller)
                .WithMany(c => c.TellerLedgerAccounts)
                .HasForeignKey(cv => cv.TellerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cv => cv.LedgerAccount)
                .WithMany(vt => vt.TellerLedgerAccounts)
                .HasForeignKey(cv => cv.LegderAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
