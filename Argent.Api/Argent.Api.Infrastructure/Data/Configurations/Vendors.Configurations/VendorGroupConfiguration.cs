using Argent.Api.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Vendors.Configurations {
    public class VendorGroupConfiguration : IEntityTypeConfiguration<VendorGroup> {
        public void Configure(EntityTypeBuilder<VendorGroup> builder) {
            builder.ToTable("vendor_groups");
            builder.Property(r => r.Code).IsRequired().HasMaxLength(20);
            builder.Property(r => r.GroupName).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Notes).IsRequired().HasMaxLength(250);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);
            builder.HasIndex(r => r.Code).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_vendor_group_code");

            builder.HasMany(cj => cj.Vendors)
                .WithOne(c => c.VendorGroup)
                .HasForeignKey(cj => cj.VendorGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
