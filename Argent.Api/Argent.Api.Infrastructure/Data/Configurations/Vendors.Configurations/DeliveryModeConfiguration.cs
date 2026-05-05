using Argent.Api.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Vendors.Configurations {
    public class DeliveryModeConfiguration : IEntityTypeConfiguration<DeliveryMode> {
        public void Configure(EntityTypeBuilder<DeliveryMode> builder) {
            builder.ToTable("delivery_mode");
            builder.Property(r => r.Code).IsRequired().HasMaxLength(10);
            builder.Property(r => r.Description).HasMaxLength(200);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);

            builder.HasMany(cj => cj.Vendors)
                .WithOne(c => c.DeliveryMode)
                .HasForeignKey(cj => cj.DeliveryModeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
