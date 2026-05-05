using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class BankLedgerEntryConfiguration : IEntityTypeConfiguration<BankLedgerEntry> {
        public void Configure(EntityTypeBuilder<BankLedgerEntry> builder) {
            builder.ToTable("acc_bank_ledger");
            builder.Property(b => b.TransactionCode).IsRequired().HasMaxLength(50);
            builder.Property(b => b.FolioCode).HasMaxLength(20);
            builder.Property(b => b.Description).HasMaxLength(300);
            builder.Property(b => b.VoucherNumber).HasMaxLength(50);
            builder.Property(b => b.LedgerCode).HasMaxLength(30);
            builder.Property(b => b.CurrencyCode).HasMaxLength(10);
            builder.Property(b => b.Debit).HasColumnType("decimal(18,2)");
            builder.Property(b => b.Credit).HasColumnType("decimal(18,2)");
            builder.Property(b => b.Balance).HasColumnType("decimal(18,2)");
            builder.Property(b => b.CreatedBy).HasMaxLength(100);
            builder.Property(b => b.UpdatedBy).HasMaxLength(100);
            builder.Property(b => b.DeletedBy).HasMaxLength(100);

            builder.HasIndex(b => b.BankAccountId).HasDatabaseName("ix_acc_bank_ledger_account");
            builder.HasIndex(b => b.TransactionCode).HasDatabaseName("ix_acc_bank_ledger_txn");
            builder.HasIndex(b => new { b.BankAccountId, b.Reconciled })
                .HasDatabaseName("ix_acc_bank_ledger_account_reconciled");

            builder.HasOne(b => b.BankAccount)
                .WithMany()
                .HasForeignKey(b => b.BankAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.TransactionDocument)
                .WithMany()
                .HasForeignKey(b => b.TransactionDocumentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

}
