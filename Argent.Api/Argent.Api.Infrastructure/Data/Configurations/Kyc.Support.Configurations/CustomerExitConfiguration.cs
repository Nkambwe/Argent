using Argent.Api.Domain.Entities.Kyc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Support.Configurations {
    public class CustomerExitConfiguration : IEntityTypeConfiguration<CustomerExit> {
        public void Configure(EntityTypeBuilder<CustomerExit> builder) {
            builder.ToTable("kyc_customer_exits");
            builder.Property(e => e.Notes).HasMaxLength(500);
            builder.Property(e => e.CreatedBy).HasMaxLength(100);
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);
            builder.Property(e => e.DeletedBy).HasMaxLength(100);
            builder.HasOne(e => e.Reason).WithMany().HasForeignKey(e => e.ReasonId).OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(e => new { e.CustomerId, e.CustomerType }).HasDatabaseName("ix_customer_exits_customer");
        }
    }
}
