using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class ChequeLedgerEntryConfiguration : IEntityTypeConfiguration<ChequeLedgerEntry> {
        public void Configure(EntityTypeBuilder<ChequeLedgerEntry> builder) {
            builder.ToTable("acc_cheque_ledger");
            builder.Property(c => c.FolioCode).HasMaxLength(20);
            builder.Property(c => c.Particulars).HasMaxLength(300);
            builder.Property(c => c.Amount).HasColumnType("decimal(18,2)");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasIndex(c => c.ChequeId).HasDatabaseName("ix_acc_cheque_ledger");

            builder.HasOne(c => c.Cheque)
                .WithMany()
                .HasForeignKey(c => c.ChequeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.BankEntries)
                .WithMany(b => b.ChequeEntries)
                .UsingEntity(j => j.ToTable("acc_cheque_bank_ledger_links"));
        }
    }

}
