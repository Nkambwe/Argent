using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class CashAccountConfiguration : IEntityTypeConfiguration<CashAccount> {
        public void Configure(EntityTypeBuilder<CashAccount> builder) {
            builder.ToTable("acc_cash_accounts");
            builder.Property(c => c.LedgerNumber).IsRequired().HasMaxLength(30);
            builder.Property(c => c.MinimumPayout).HasColumnType("decimal(18,2)");
            builder.Property(c => c.MaximumPayout).HasColumnType("decimal(18,2)");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasIndex(c => c.LedgerAccountId).HasDatabaseName("ix_acc_cash_accounts_ledger");

            builder.HasOne(c => c.LedgerAccount)
                .WithMany()
                .HasForeignKey(c => c.LedgerAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
