using Argent.Api.Domain.Entities.Accounting.Vouchers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {

    public class VoucherLineConfiguration : IEntityTypeConfiguration<VoucherEntry> {

        public void Configure(EntityTypeBuilder<VoucherEntry> builder) {
            builder.ToTable("acc_voucher_lines");
            builder.Property(v => v.TransactionId).IsRequired().HasMaxLength(50);
            builder.Property(v => v.Particulars).HasMaxLength(300);
            builder.Property(v => v.FolioCode).HasMaxLength(20);
            builder.Property(v => v.VoucherNumber).HasMaxLength(50);
            builder.Property(v => v.RelatesTo).HasMaxLength(50);
            builder.Property(v => v.Debit).HasColumnType("decimal(18,2)");
            builder.Property(v => v.Credit).HasColumnType("decimal(18,2)");
            builder.Property(v => v.Discount).HasColumnType("decimal(18,2)");
            builder.Property(v => v.Authorized).HasMaxLength(150);
            builder.Property(v => v.CashierCode).HasMaxLength(20);
            builder.Property(v => v.CreatedBy).HasMaxLength(100);
            builder.Property(v => v.UpdatedBy).HasMaxLength(100);
            builder.Property(v => v.DeletedBy).HasMaxLength(100);
            builder.HasIndex(v => v.TransactionId).HasDatabaseName("ix_acc_voucher_lines_txn");
            builder.HasIndex(v => v.PostedOn).HasDatabaseName("ix_acc_voucher_lines_posted_on");

            builder.HasOne(v => v.VoucherType)
                .WithMany(vt => vt.Vouchers)
                .HasForeignKey(v => v.VoucherTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.TransactionDocumentType)
                .WithMany(v => v.VoucherEntries)
                .HasForeignKey(v => v.TransactionDocumentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.TransactionDocument)
                .WithMany(v => v.VoucherEntries)
                .HasForeignKey(v => v.TransactionDocumentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.GeneralLedgerEntry)
                .WithMany(l => l.VoucherLines)
                .HasForeignKey(v => v.GeneralLedgerEntryId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }

}
