using Argent.Api.Domain.Entities.Banking.Loans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class LoanOfficerLedgerAccountConfiguration : IEntityTypeConfiguration<LoanOfficerLedgerAccount> {
        public void Configure(EntityTypeBuilder<LoanOfficerLedgerAccount> builder) {
            builder.ToTable("lnr_loan_officer_acc");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasIndex(cv => new { cv.LoanOfficerId, cv.LegderAccountId }).IsUnique()
                .HasFilter("\"IsDeleted\" = false")
                .HasDatabaseName("ux_lnr_loan_officer_ledger");

            builder.HasOne(cv => cv.LoanOfficer)
                .WithMany(c => c.LoanOfficerLedgerAccounts)
                .HasForeignKey(cv => cv.LoanOfficerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cv => cv.LedgerAccount)
                .WithMany(vt => vt.LoanOfficerLedgerAccounts)
                .HasForeignKey(cv => cv.LegderAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
