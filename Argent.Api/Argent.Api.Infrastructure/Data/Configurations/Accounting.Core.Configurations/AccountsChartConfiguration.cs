using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Core.Configurations {
    public class AccountsChartConfiguration : IEntityTypeConfiguration<AccountsChart> {
        public void Configure(EntityTypeBuilder<AccountsChart> builder) {
            builder.ToTable("acc_charts");
            builder.Property(c => c.ChartName).IsRequired().HasMaxLength(150);
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);
            builder.HasIndex(c => c.ChartName).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_charts_name");
        }
    }
}
