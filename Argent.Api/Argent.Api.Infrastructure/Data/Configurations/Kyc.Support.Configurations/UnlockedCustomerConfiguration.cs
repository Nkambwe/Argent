using Argent.Api.Domain.Entities.Kyc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Support.Configurations {
    public class UnlockedCustomerConfiguration : IEntityTypeConfiguration<UnlockedCustomer> {
        public void Configure(EntityTypeBuilder<UnlockedCustomer> builder) {
            builder.ToTable("kyc_unlocked_customers");
            builder.Property(u => u.UnlockedBy).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Notes).HasMaxLength(500);
            builder.Property(u => u.CreatedBy).HasMaxLength(100);
            builder.Property(u => u.UpdatedBy).HasMaxLength(100);
            builder.Property(u => u.DeletedBy).HasMaxLength(100);
            builder.HasOne(u => u.Reason).WithMany().HasForeignKey(u => u.ReasonId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
