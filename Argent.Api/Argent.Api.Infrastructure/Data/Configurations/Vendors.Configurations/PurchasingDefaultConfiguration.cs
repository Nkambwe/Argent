using Argent.Api.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Vendors.Configurations {
    public class PurchasingDefaultConfiguration : IEntityTypeConfiguration<PurchasingDefault> {
        public void Configure(EntityTypeBuilder<PurchasingDefault> builder) {
            builder.ToTable("purchase_defaults");
            builder.Property(r => r.ContactPerson).IsRequired().HasMaxLength(180); 
            builder.Property(r => r.ReferenceGroup).HasMaxLength(20);
            builder.Property(r => r.ReferenceValue).HasMaxLength(20);
            builder.Property(r => r.PurchaseOfficer).HasMaxLength(120);
            builder.Property(r => r.Currency).HasMaxLength(3);
            builder.Property(r => r.Notes).HasMaxLength(250);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);

            builder.HasOne(cj => cj.Vendor)
                .WithMany(c => c.PurchasingDefaults)
                .HasForeignKey(cj => cj.VendorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
