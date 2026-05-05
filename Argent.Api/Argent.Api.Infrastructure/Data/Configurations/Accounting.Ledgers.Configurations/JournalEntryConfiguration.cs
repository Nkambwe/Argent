using Argent.Api.Domain.Entities.Accounting.Journals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class JournalEntryConfiguration : IEntityTypeConfiguration<JournalEntry> {
        public void Configure(EntityTypeBuilder<JournalEntry> builder) {
            builder.ToTable("acc_journal_entries");
            builder.Property(j => j.TransactionCode).IsRequired().HasMaxLength(50);
            builder.Property(j => j.Particulars).HasMaxLength(300);
            builder.Property(j => j.FolioCode).HasMaxLength(20);
            builder.Property(j => j.LedgerNumber).HasMaxLength(30);
            builder.Property(j => j.PostingSeries).HasMaxLength(20);
            builder.Property(j => j.VoucherNumber).HasMaxLength(50);
            builder.Property(j => j.Debit).HasColumnType("decimal(18,2)");
            builder.Property(j => j.Credit).HasColumnType("decimal(18,2)");
            builder.Property(j => j.CurrencyCode).HasMaxLength(10);
            builder.Property(j => j.ExchangeAmount).HasColumnType("decimal(18,4)");
            builder.Property(j => j.GeneralReference).HasMaxLength(50);
            builder.Property(j => j.BusinessReference).HasMaxLength(50);
            builder.Property(j => j.ChargeReference).HasMaxLength(50);
            builder.Property(j => j.Reference1).HasMaxLength(50);
            builder.Property(j => j.Reference2).HasMaxLength(50);
            builder.Property(j => j.Reference3).HasMaxLength(50);
            builder.Property(j => j.Reference4).HasMaxLength(50);
            builder.Property(j => j.Reference5).HasMaxLength(50);
            builder.Property(j => j.Reference6).HasMaxLength(50);
            builder.Property(j => j.TaxCode).HasMaxLength(20);
            builder.Property(j => j.TaxCharge1).HasColumnType("decimal(18,2)");
            builder.Property(j => j.TaxCharge2).HasColumnType("decimal(18,2)");
            builder.Property(j => j.Comment).HasMaxLength(300);
            builder.Property(j => j.Cashier).HasMaxLength(50);
            builder.Property(j => j.ApprovedBy).HasMaxLength(150);
            builder.Property(j => j.CreatedBy).HasMaxLength(100);
            builder.Property(j => j.UpdatedBy).HasMaxLength(100);
            builder.Property(j => j.DeletedBy).HasMaxLength(100);

            builder.HasIndex(j => j.TransactionCode).HasDatabaseName("ix_acc_journal_entries_txn");
            builder.HasIndex(j => j.PostedOn).HasDatabaseName("ix_acc_journal_entries_posted_on");
            builder.HasIndex(j => new { j.UnPosted, j.Approved })
                .HasDatabaseName("ix_acc_journal_entries_status");

            builder.HasOne(j => j.JournalType)
                .WithMany(jt => jt.Journals)
                .HasForeignKey(j => j.JournalTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.GeneralLedgerEntry)
                .WithMany()
                .HasForeignKey(c => c.GeneralLedgerEntryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
