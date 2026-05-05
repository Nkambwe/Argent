using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class RegistrationLedgerEntryConfiguration : IEntityTypeConfiguration<RegistrationLedgerEntry> {
        public void Configure(EntityTypeBuilder<RegistrationLedgerEntry> builder) {
            builder.ToTable("acc_registration_ledger");
            builder.Property(c => c.TransactionCode).HasMaxLength(20);
            builder.Property(c => c.ClientCode).IsRequired().HasMaxLength(10);
            builder.Property(c => c.PostedOn).IsRequired();
            builder.Property(c => c.Amount).HasColumnType("decimal(18,2)");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasOne(c => c.ChargeItem)
                .WithMany(l => l.RegistrationLedgerEntries)
                .HasForeignKey(c => c.ChargeItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
