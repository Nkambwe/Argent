using Argent.Api.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class DiscountGroupConfiguration : IEntityTypeConfiguration<DiscountGroup> {
        public void Configure(EntityTypeBuilder<DiscountGroup> builder) {
            builder.ToTable("discount_groups");
            builder.Property(r => r.Code).IsRequired().HasMaxLength(20);
            builder.Property(r => r.GroupName).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Notes).IsRequired().HasMaxLength(250);
            builder.Property(r => r.Type);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);
            builder.HasIndex(r => r.Code).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_discount_groups");

            builder.HasMany(cj => cj.PurchaseOrderDefaults)
                .WithOne(c => c.DiscountGroup)
                .HasForeignKey(cj => cj.DiscountGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
