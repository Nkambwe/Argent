using Argent.Api.Domain.Entities.Support.KycSupport;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Support.Configurations {
    public class RejectedCustomerConfiguration : IEntityTypeConfiguration<RejectedCustomer> {
        public void Configure(EntityTypeBuilder<RejectedCustomer> builder) {
            builder.ToTable("kyc_rejected_customers");
            builder.Property(r => r.RejectedBy).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Notes).HasMaxLength(500);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);
            builder.HasOne(r => r.Reason).WithMany().HasForeignKey(r => r.ReasonId).OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(r => new { r.CustomerId, r.CustomerType }).HasDatabaseName("ix_rejected_customers_customer");
        }
    }
}
