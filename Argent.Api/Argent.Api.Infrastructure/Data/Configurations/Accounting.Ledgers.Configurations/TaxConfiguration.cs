using Argent.Api.Domain.Entities.Accounting.Taxes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class TaxConfiguration : IEntityTypeConfiguration<Tax> {
        public void Configure(EntityTypeBuilder<Tax> builder) {
            builder.ToTable("acc_taxes");
            builder.Property(r => r.Code).IsRequired().HasMaxLength(20);
            builder.Property(r => r.Description).IsRequired().HasMaxLength(150);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);
            builder.HasIndex(r => r.Code).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_taxes_code");
        }
    }
}
