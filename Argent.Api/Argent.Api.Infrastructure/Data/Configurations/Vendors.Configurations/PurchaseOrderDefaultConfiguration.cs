using Argent.Api.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Vendors.Configurations {
    public class PurchaseOrderDefaultConfiguration : IEntityTypeConfiguration<PurchaseOrderDefault> {
        public void Configure(EntityTypeBuilder<PurchaseOrderDefault> builder) {
            builder.ToTable("purchase_order_defaults");
            builder.Property(r => r.MultiBranchAccount).HasMaxLength(40);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);

            builder.HasOne(cj => cj.VendorGroup)
                .WithMany(c => c.PurchaseOrderDefaults)
                .HasForeignKey(cj => cj.VendorGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cj => cj.Vendor)
                .WithMany(c => c.PurchaseOrderDefaults)
                .HasForeignKey(cj => cj.VendorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
