using Argent.Api.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Vendors.Configurations {

    public class PurchaseOrderClassificationConfiguration : IEntityTypeConfiguration<PurchaseOrderClassification> {
        public void Configure(EntityTypeBuilder<PurchaseOrderClassification> builder) {
            builder.ToTable("purchase_order_classifications");
            builder.Property(r => r.Code).IsRequired().HasMaxLength(10);
            builder.Property(r => r.Name).HasMaxLength(120);
            builder.Property(r => r.Notes).HasMaxLength(250);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);

            builder.HasMany(cj => cj.PurchaseOrderDefaults)
                .WithOne(c => c.PurchaseOrderClassification)
                .HasForeignKey(cj => cj.PurchaseOrderClassificationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
