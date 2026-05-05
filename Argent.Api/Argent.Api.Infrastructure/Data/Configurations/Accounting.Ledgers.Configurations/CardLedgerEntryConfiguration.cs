using Argent.Api.Domain.Entities.Accounting;
using Argent.Api.Domain.Entities.Banking.Loans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class CardLedgerEntryConfiguration : IEntityTypeConfiguration<CardLedgerEntry> {
        public void Configure(EntityTypeBuilder<CardLedgerEntry> builder) {
            builder.ToTable("acc_card_ledger");
            builder.Property(c => c.ExternalTransactionId).HasMaxLength(100);
            builder.Property(c => c.Particulars).HasMaxLength(300);
            builder.Property(c => c.FolioCode).HasMaxLength(20);
            builder.Property(c => c.Amount).HasColumnType("decimal(18,2)");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasIndex(c => c.CardId).HasDatabaseName("ix_acc_card_ledger_card");

            builder.HasOne(c => c.Card)
                .WithMany()
                .HasForeignKey(c => c.CardId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.GeneralLedgerEntry)
                .WithMany(l => l.CardEntries)
                .HasForeignKey(c => c.GeneralLedgerEntryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class LoanOfficerConfiguration : IEntityTypeConfiguration<LoanOfficer> {
        public void Configure(EntityTypeBuilder<LoanOfficer> builder) {
            builder.ToTable("lnr_loan_officer");
            builder.Property(c => c.LoanOfficerCode).IsRequired().HasMaxLength(10);
            builder.Property(c => c.ApprovalLimit).HasColumnType("decimal(18,2)");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasIndex(c => c.LoanOfficerCode).HasDatabaseName("ix_loan_officer_code");

            builder.HasOne(c => c.AppUser)
                .WithMany()
                .HasForeignKey(c => c.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.LoanOfficerLedgerAccounts)
                .WithOne(l => l.LoanOfficer)
                .HasForeignKey(c => c.LoanOfficerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
