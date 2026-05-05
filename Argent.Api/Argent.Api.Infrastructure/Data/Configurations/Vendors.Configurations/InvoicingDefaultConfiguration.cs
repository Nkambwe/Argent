using Argent.Api.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Vendors.Configurations {
    public class InvoicingDefaultConfiguration : IEntityTypeConfiguration<InvoicingDefault> {
        public void Configure(EntityTypeBuilder<InvoicingDefault> builder) {
            builder.ToTable("invoicing_defaults");
            builder.Property(r => r.MultiBranchInvoiceAccount).IsRequired(false).HasMaxLength(20);
            builder.Property(r => r.InvoicingLedger).IsRequired(false).HasMaxLength(20);
            builder.Property(r => r.InvoicingAddress).IsRequired(false).HasMaxLength(180);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);

            builder.HasOne(cj => cj.Vendor)
                .WithMany(c => c.InvoicingDefaults)
                .HasForeignKey(cj => cj.VendorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
