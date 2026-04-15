using Argent.Api.Domain.Entities.Kyc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Support.Configurations {
    public class CustomerApprovalConfiguration : IEntityTypeConfiguration<CustomerApproval> {
        public void Configure(EntityTypeBuilder<CustomerApproval> builder) {
            builder.ToTable("kyc_customer_approvals");
            builder.Property(a => a.ActionedBy).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Comments).HasMaxLength(500);
            builder.Property(a => a.Notes).HasMaxLength(500);
            builder.Property(a => a.CreatedBy).HasMaxLength(100);
            builder.Property(a => a.UpdatedBy).HasMaxLength(100);
            builder.Property(a => a.DeletedBy).HasMaxLength(100);
            builder.HasIndex(a => new { a.CustomerId, a.CustomerType }).HasDatabaseName("ix_customer_approvals_customer");
        }
    }
}
