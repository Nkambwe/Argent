using Argent.Api.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Vendors.Configurations {
    public class PaymentDefaultConfiguration : IEntityTypeConfiguration<PaymentDefault> {
        public void Configure(EntityTypeBuilder<PaymentDefault> builder) {
            builder.ToTable("payment_defaults");
            builder.Property(r => r.PaymentMethod).IsRequired();
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);

            builder.HasOne(cj => cj.PaymentTerm)
                .WithMany(c => c.PaymentDefaults)
                .HasForeignKey(cj => cj.PaymentTermId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cj => cj.Vendor)
                .WithMany(c => c.VendorPaymentDefaults)
                .HasForeignKey(cj => cj.VendorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cj => cj.BankAccount)
                .WithMany(c => c.PaymentDefault)
                .HasForeignKey(cj => cj.BankAccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
