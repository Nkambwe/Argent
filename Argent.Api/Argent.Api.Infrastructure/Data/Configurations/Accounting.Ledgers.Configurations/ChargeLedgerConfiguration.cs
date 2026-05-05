using Argent.Api.Domain.Entities.Accounting.Charges;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class ChargeLedgerConfiguration : IEntityTypeConfiguration<ChargeLedger> {
        public void Configure(EntityTypeBuilder<ChargeLedger> builder) {
            builder.ToTable("acc_charge_ledger");
            builder.Property(c => c.TransactionCode).HasMaxLength(20);
            builder.Property(c => c.Series).HasMaxLength(20);
            builder.Property(c => c.Client).HasMaxLength(20);
            builder.Property(c => c.LoanNumber).HasMaxLength(20);
            builder.Property(c => c.Product).HasMaxLength(20);
            builder.Property(c => c.LedgerNumber).HasMaxLength(20);
            builder.Property(c => c.PostedOn);
            builder.Property(c => c.Amount).HasColumnType("decimal(18,2)");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasOne(c => c.ChargeItem)
                .WithMany()
                .HasForeignKey(c => c.ChargeItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
