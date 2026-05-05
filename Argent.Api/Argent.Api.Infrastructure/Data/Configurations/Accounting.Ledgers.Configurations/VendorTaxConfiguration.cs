using Argent.Api.Domain.Entities.Accounting.Taxes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class VendorTaxConfiguration : IEntityTypeConfiguration<VendorTax> {
        public void Configure(EntityTypeBuilder<VendorTax> builder) {
            builder.ToTable("acc_vendor_taxes");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasIndex(cj => new { cj.VendorId, cj.TaxId }).IsUnique()
                .HasFilter("\"IsDeleted\" = false")
                .HasDatabaseName("ux_acc_vendor_taxes");

            builder.HasOne(cj => cj.Vendor)
                .WithMany(c => c.Taxes)
                .HasForeignKey(cj => cj.TaxId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cj => cj.Tax)
                .WithMany(jt => jt.Vendors)
                .HasForeignKey(cj => cj.VendorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
