using Argent.Api.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Vendors.Configurations {
    public class VendorAddressConfiguration : IEntityTypeConfiguration<VendorAddress> {
        public void Configure(EntityTypeBuilder<VendorAddress> builder) {
            builder.ToTable("vendor_addresses");
            builder.Property(r => r.Address).IsRequired().HasMaxLength(512);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);

            builder.HasOne(cj => cj.Vendor)
                .WithMany(c => c.VendorAddresses)
                .HasForeignKey(cj => cj.VendorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
