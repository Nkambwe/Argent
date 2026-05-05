using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Core.Configurations {
    public class MonthlyClosureConfiguration : IEntityTypeConfiguration<MonthlyClosure> {
        public void Configure(EntityTypeBuilder<MonthlyClosure> builder) {
            builder.ToTable("acc_monthly_closures");
            builder.Property(m => m.CreatedBy).HasMaxLength(100);
            builder.Property(m => m.UpdatedBy).HasMaxLength(100);
            builder.Property(m => m.DeletedBy).HasMaxLength(100);

            builder.HasIndex(m => new { m.FinancialYearId, m.Month }).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_monthly_closures_year_month");

            builder.HasOne(m => m.FinancialYear)
                .WithMany(y => y.MonthlyClosures)
                .HasForeignKey(m => m.FinancialYearId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
