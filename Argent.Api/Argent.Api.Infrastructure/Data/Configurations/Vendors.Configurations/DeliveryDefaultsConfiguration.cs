using Argent.Api.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Vendors.Configurations {
    public class DeliveryDefaultsConfiguration : IEntityTypeConfiguration<DeliveryDefaults> {
        public void Configure(EntityTypeBuilder<DeliveryDefaults> builder) {
            builder.ToTable("delivery_defaults");
            builder.Property(r => r.Receiver).IsRequired().HasMaxLength(120);
            builder.Property(r => r.ReferenceGroup).HasMaxLength(40);
            builder.Property(r => r.ReferenceValue).HasMaxLength(40);
            builder.Property(r => r.DeliveryAddress).HasMaxLength(100);
            builder.Property(r => r.Currency).IsRequired().HasMaxLength(3);
            builder.Property(r => r.Notes).HasMaxLength(250);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);

            builder.HasOne(cj => cj.Vendor)
                .WithMany(c => c.DeliveryDefaults)
                .HasForeignKey(cj => cj.VendorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
