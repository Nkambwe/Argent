using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class SwiftConfiguration : IEntityTypeConfiguration<Swift> {
        public void Configure(EntityTypeBuilder<Swift> builder) {
            builder.ToTable("acc_swifts");
            builder.Property(s => s.Code).IsRequired().HasMaxLength(20);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.CreatedBy).HasMaxLength(100);
            builder.Property(s => s.UpdatedBy).HasMaxLength(100);
            builder.Property(s => s.DeletedBy).HasMaxLength(100);
            builder.HasIndex(s => s.Code).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_swifts_code");
        }
    }
}
