using Argent.Api.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class VendorReferenceConfiguration : IEntityTypeConfiguration<VendorReference> {
        public void Configure(EntityTypeBuilder<VendorReference> builder) {
            builder.ToTable("acc_vendor_references");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasIndex(cj => new { cj.VendorId, cj.ReferenceValueId }).IsUnique()
                .HasFilter("\"IsDeleted\" = false")
                .HasDatabaseName("ux_acc_vendor_reference_values");

            builder.HasOne(cj => cj.Vendor)
                .WithMany(c => c.RefereceValues)
                .HasForeignKey(cj => cj.ReferenceValueId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cj => cj.ReferenceValue)
                .WithMany(jt => jt.VendorReferences)
                .HasForeignKey(cj => cj.VendorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
