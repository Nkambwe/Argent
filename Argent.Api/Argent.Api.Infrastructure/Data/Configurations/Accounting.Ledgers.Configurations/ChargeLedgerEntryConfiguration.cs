using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class ChargeLedgerEntryConfiguration : IEntityTypeConfiguration<ChargeLedgerEntry> {
        public void Configure(EntityTypeBuilder<ChargeLedgerEntry> builder) {
            builder.ToTable("acc_charge_ledger_entry");
            builder.Property(c => c.TransactionCode).HasMaxLength(20);
            builder.Property(c => c.Series).IsRequired(false).HasMaxLength(10);
            builder.Property(c => c.Customer).IsRequired(false).HasMaxLength(10);
            builder.Property(c => c.LoanNumber).IsRequired(false).HasMaxLength(10);
            builder.Property(c => c.Product).IsRequired(false).HasMaxLength(10);
            builder.Property(c => c.LedgerNumber).IsRequired(false).HasMaxLength(10);
            builder.Property(c => c.Amount).HasColumnType("decimal(18,2)");
            builder.Property(c => c.PostedOn).IsRequired();
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasOne(c => c.ChargeItem)
                .WithMany(l => l.ChargeLedgerEntries)
                .HasForeignKey(c => c.ChargeItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
