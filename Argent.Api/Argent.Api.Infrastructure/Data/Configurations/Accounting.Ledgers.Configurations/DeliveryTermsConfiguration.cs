using Argent.Api.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class DeliveryTermsConfiguration : IEntityTypeConfiguration<DeliveryTerms> {
        public void Configure(EntityTypeBuilder<DeliveryTerms> builder) {
            builder.ToTable("vendor_delivery_terms");
            builder.Property(r => r.Code).IsRequired().HasMaxLength(20);
            builder.Property(r => r.Description).IsRequired().HasMaxLength(150);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);
            builder.HasIndex(r => r.Code).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_vendor_delivery_terms");

            builder.HasMany(cj => cj.Vendors)
                .WithOne(c => c.DeliveryTerm)
                .HasForeignKey(cj => cj.DeliverTermsId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
