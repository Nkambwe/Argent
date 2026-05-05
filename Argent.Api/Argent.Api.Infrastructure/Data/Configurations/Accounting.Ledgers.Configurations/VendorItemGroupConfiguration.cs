using Argent.Api.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class VendorItemGroupConfiguration : IEntityTypeConfiguration<VendorItemGroup> {
        public void Configure(EntityTypeBuilder<VendorItemGroup> builder) {
            builder.ToTable("vendor_item_groups");
            builder.Property(r => r.Code).IsRequired().HasMaxLength(20);
            builder.Property(r => r.ItemGroup).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Notes).IsRequired().HasMaxLength(250);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);
            builder.HasIndex(r => r.Code).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_vendor_item_groups");

            builder.HasMany(cj => cj.PurchaseOrderDefaults)
                .WithOne(c => c.VendorItemGroup)
                .HasForeignKey(cj => cj.VendorItemGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
