using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class LedgerRecurringItemConfiguration : IEntityTypeConfiguration<LedgerRecurringItem> {
        public void Configure(EntityTypeBuilder<LedgerRecurringItem> builder) {
            builder.ToTable("acc_ledger_recurring_items");
            builder.Property(r => r.Code).IsRequired().HasMaxLength(20);
            builder.Property(r => r.Name).IsRequired().HasMaxLength(150);
            builder.Property(r => r.Amount).HasColumnType("decimal(18,2)");
            builder.Property(r => r.DebitLedger).IsRequired().HasMaxLength(30);
            builder.Property(r => r.CreditLedger).IsRequired().HasMaxLength(30);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);

            builder.HasIndex(r => r.Code).IsUnique()
                .HasFilter("\"IsDeleted\" = false")
                .HasDatabaseName("ux_acc_recurring_items_code");

            builder.HasOne(r => r.Branch)
                .WithMany()
                .HasForeignKey(r => r.BranchId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

}
