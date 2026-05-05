using Argent.Api.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Vendors.Configurations {
    public class VendorConfiguration : IEntityTypeConfiguration<Vendor> {
        public void Configure(EntityTypeBuilder<Vendor> builder) {
            builder.ToTable("vendor_addresses");
            builder.Property(r => r.Series).IsRequired().HasMaxLength(10);
            builder.Property(r => r.Name).IsRequired().HasMaxLength(250);
            builder.Property(r => r.Alias).IsRequired().HasMaxLength(250);
            builder.Property(r => r.Language).IsRequired().HasMaxLength(50);
            builder.Property(r => r.LedgerAccount).IsRequired().HasMaxLength(10);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);

            builder.HasOne(cj => cj.VendorGroup)
                .WithMany(c => c.Vendors)
                .HasForeignKey(cj => cj.VendorGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cj => cj.DeliveryTerm)
                .WithMany(c => c.Vendors)
                .HasForeignKey(cj => cj.DeliverTermsId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cj => cj.DeliveryMode)
                .WithMany(c => c.Vendors)
                .HasForeignKey(cj => cj.DeliveryModeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
