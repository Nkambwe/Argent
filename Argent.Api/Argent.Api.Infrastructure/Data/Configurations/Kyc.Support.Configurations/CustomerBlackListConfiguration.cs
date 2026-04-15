using Argent.Api.Domain.Entities.Kyc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Support.Configurations {
    public class CustomerBlackListConfiguration : IEntityTypeConfiguration<CustomerBlackList> {
        public void Configure(EntityTypeBuilder<CustomerBlackList> builder) {
            builder.ToTable("kyc_customer_blacklists");
            builder.Property(b => b.Notes).HasMaxLength(500);
            builder.Property(b => b.CreatedBy).HasMaxLength(100);
            builder.Property(b => b.UpdatedBy).HasMaxLength(100);
            builder.Property(b => b.DeletedBy).HasMaxLength(100);
            builder.HasOne(b => b.Reason).WithMany().HasForeignKey(b => b.ReasonId).OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(b => new { b.CustomerId, b.CustomerType }).HasDatabaseName("ix_customer_blacklists_customer");
        }
    }
}
