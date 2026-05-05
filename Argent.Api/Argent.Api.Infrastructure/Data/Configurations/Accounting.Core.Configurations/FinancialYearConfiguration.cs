using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Core.Configurations {
    public class FinancialYearConfiguration : IEntityTypeConfiguration<FinancialYear> {
        public void Configure(EntityTypeBuilder<FinancialYear> builder) {
            builder.ToTable("acc_financial_years");
            builder.Property(y => y.Code).IsRequired().HasMaxLength(20);
            builder.Property(y => y.YearName).IsRequired().HasMaxLength(50);
            builder.Property(y => y.CreatedBy).HasMaxLength(100);
            builder.Property(y => y.UpdatedBy).HasMaxLength(100);
            builder.Property(y => y.DeletedBy).HasMaxLength(100);

            // One open year per branch (or org-wide if BranchId is null)
            builder.HasIndex(y => new { y.BranchId, y.Closed })
                .HasDatabaseName("ix_acc_financial_years_branch_closed");

            builder.HasOne(y => y.Branch)
                .WithMany()
                .HasForeignKey(y => y.BranchId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
