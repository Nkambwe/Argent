using Argent.Api.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Vendors.Configurations {
    public class PaymentTermConfiguration : IEntityTypeConfiguration<PaymentTerm> {
        public void Configure(EntityTypeBuilder<PaymentTerm> builder) {
            builder.ToTable("payment_terms");
            builder.Property(r => r.Terms).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Description).HasMaxLength(200);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);

            builder.HasMany(cj => cj.PaymentDefaults)
                .WithOne(c => c.PaymentTerm)
                .HasForeignKey(cj => cj.PaymentTermId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
