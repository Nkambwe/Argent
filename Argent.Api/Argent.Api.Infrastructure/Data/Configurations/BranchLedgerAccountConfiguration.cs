using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations {
    public class BranchLedgerAccountConfiguration : IEntityTypeConfiguration<BranchLedgerAccount> {
        public void Configure(EntityTypeBuilder<BranchLedgerAccount> builder) {
            builder.ToTable("acc_branch_ledger");
            builder.Property(c => c.BranchAccountNumber).IsRequired().HasMaxLength(10);
            builder.Property(c => c.LedgerAccountNumber).IsRequired().HasMaxLength(10);
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);
            builder.Property(c => c.BranchId).IsRequired(false).HasMaxLength(80);

            builder.HasOne(c => c.Branch)
                .WithMany(c => c.BranchLedgerAccounts)
                .HasForeignKey(y => y.BranchId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(c => c.LedgerAccount)
                .WithMany(c => c.BranchLedgerAccounts)
                .HasForeignKey(y => y.BranchId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}


