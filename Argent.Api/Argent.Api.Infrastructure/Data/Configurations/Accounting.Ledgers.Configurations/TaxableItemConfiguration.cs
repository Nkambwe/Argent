using Argent.Api.Domain.Entities.Accounting.Taxes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class TaxableItemConfiguration : IEntityTypeConfiguration<TaxableItem> {
        public TaxableItemConfiguration() {
        }

        public void Configure(EntityTypeBuilder<TaxableItem> builder) {
            builder.ToTable("acc_taxe_items");
            builder.Property(r => r.ItemCode).IsRequired().HasMaxLength(20);
            builder.Property(r => r.Item).IsRequired().HasMaxLength(150);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);
            builder.HasIndex(r => r.ItemCode).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_tax_items_code");
        }
    }

}
