using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class GeneralLedgerEntryConfiguration : IEntityTypeConfiguration<GeneralLedgerEntry> {
        public void Configure(EntityTypeBuilder<GeneralLedgerEntry> builder) {
            builder.ToTable("acc_general_ledger");
            builder.Property(l => l.TransactionCode).IsRequired().HasMaxLength(50);
            builder.Property(l => l.Particulars).HasMaxLength(300);
            builder.Property(l => l.FolioCode).HasMaxLength(20);
            builder.Property(l => l.LedgerNumber).IsRequired().HasMaxLength(30);
            builder.Property(l => l.PostingSeries).HasMaxLength(20);
            builder.Property(l => l.VoucherNumber).HasMaxLength(50);
            builder.Property(l => l.Debit).HasColumnType("decimal(18,2)");
            builder.Property(l => l.Credit).HasColumnType("decimal(18,2)");
            builder.Property(l => l.CurrencyCode).HasMaxLength(10);
            builder.Property(l => l.ExchangeAmount).HasColumnType("decimal(18,4)");
            builder.Property(l => l.GeneralReference).HasMaxLength(50);
            builder.Property(l => l.BusinessReference).HasMaxLength(50);
            builder.Property(l => l.ChargeReference).HasMaxLength(50);
            builder.Property(l => l.Reference1).HasMaxLength(50);
            builder.Property(l => l.Reference2).HasMaxLength(50);
            builder.Property(l => l.Reference3).HasMaxLength(50);
            builder.Property(l => l.Reference4).HasMaxLength(50);
            builder.Property(l => l.Reference5).HasMaxLength(50);
            builder.Property(l => l.Reference6).HasMaxLength(50);
            builder.Property(l => l.TaxCode).HasMaxLength(20);
            builder.Property(l => l.TaxCharge1).HasColumnType("decimal(18,2)");
            builder.Property(l => l.TaxCharge2).HasColumnType("decimal(18,2)");
            builder.Property(l => l.Comment).HasMaxLength(300);
            builder.Property(l => l.Cashier).HasMaxLength(50);
            builder.Property(l => l.CreatedBy).HasMaxLength(100);
            builder.Property(l => l.UpdatedBy).HasMaxLength(100);
            builder.Property(l => l.DeletedBy).HasMaxLength(100);

            // Core query indexes — GL is the most queried table in the system
            builder.HasIndex(l => l.TransactionCode).HasDatabaseName("ix_acc_gl_txn_code");
            builder.HasIndex(l => l.LedgerAccountId).HasDatabaseName("ix_acc_gl_ledger_account");
            builder.HasIndex(l => l.PostedOn).HasDatabaseName("ix_acc_gl_posted_on");
            builder.HasIndex(l => l.MonthlyClosureId).HasDatabaseName("ix_acc_gl_period");
            builder.HasIndex(l => new { l.LedgerAccountId, l.PostedOn }).HasDatabaseName("ix_acc_gl_account_date");

            builder.HasOne(l => l.LedgerAccount)
                .WithMany()
                .HasForeignKey(l => l.LedgerAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.TaxGroup)
                .WithMany()
                .HasForeignKey(c => c.TaxGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.VoucherLines)
               .WithOne(v => v.GeneralLedgerEntry)
               .HasForeignKey(c => c.GeneralLedgerEntryId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.JournalEntries)
               .WithOne(j => j.GeneralLedgerEntry)
               .HasForeignKey(c => c.GeneralLedgerEntryId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
