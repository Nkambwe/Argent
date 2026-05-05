using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class CashierAccountConfiguration : IEntityTypeConfiguration<CashierAccount> {
        public void Configure(EntityTypeBuilder<CashierAccount> builder) {
            builder.ToTable("acc_cashier_accounts");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasIndex(ca => new { ca.CashierId, ca.CashAccountId }).IsUnique()
                .HasFilter("\"IsDeleted\" = false")
                .HasDatabaseName("ux_acc_cashier_accounts");

            builder.HasOne(ca => ca.Cashier)
                .WithMany(c => c.CashierAccounts)
                .HasForeignKey(ca => ca.CashierId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ca => ca.CashAccount)
                .WithMany(ca => ca.CashierAccounts)
                .HasForeignKey(ca => ca.CashAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
