using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Core.Configurations {
    public class LedgerAccountConfiguration : IEntityTypeConfiguration<LedgerAccount> {
        public void Configure(EntityTypeBuilder<LedgerAccount> builder) {
            builder.ToTable("acc_ledger_accounts");
            builder.Property(a => a.LedgerNumber).IsRequired().HasMaxLength(30);
            builder.Property(a => a.LedgerName).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Balance).HasColumnType("decimal(18,4)");
            builder.Property(a => a.Notes).HasMaxLength(500);
            builder.Property(a => a.CreatedBy).HasMaxLength(100);
            builder.Property(a => a.UpdatedBy).HasMaxLength(100);
            builder.Property(a => a.DeletedBy).HasMaxLength(100);

            builder.HasIndex(a => a.LedgerNumber).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_ledger_accounts_number");
            builder.HasIndex(a => a.LedgerAccountHeaderId)
                .HasDatabaseName("ix_acc_ledger_accounts_header");
            builder.HasIndex(a => a.AccountsChartId)
                .HasDatabaseName("ix_acc_ledger_accounts_chart");

            builder.HasOne(a => a.LedgerAccountHeader)
                .WithMany(h => h.LedgerAccounts)
                .HasForeignKey(a => a.LedgerAccountHeaderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.LedgerAccountHeader)
                .WithMany(h => h.LedgerAccounts)
                .HasForeignKey(a => a.LedgerAccountHeaderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.TellerLedgerAccounts)
                .WithOne(c => c.LedgerAccount)
                .HasForeignKey(a => a.TellerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.LoanOfficerLedgerAccounts)
                .WithOne(c => c.LedgerAccount)
                .HasForeignKey(a => a.LegderAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Folio)
                .WithMany(f => f.LedgerAccounts)
                .HasForeignKey(a => a.FolioId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(a => a.CashAccounts)
                .WithOne(f => f.LedgerAccount)
                .HasForeignKey(a => a.LedgerAccountId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(a => a.BankAccounts)
                .WithOne(f => f.LedgerAccount)
                .HasForeignKey(a => a.LedgerAccountId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(a => a.BranchLedgerAccounts)
                .WithOne(f => f.LedgerAccount)
                .HasForeignKey(a => a.LedgerAccountId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
