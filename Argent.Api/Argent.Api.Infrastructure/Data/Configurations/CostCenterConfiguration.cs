using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations {
    public class CostCenterConfiguration : IEntityTypeConfiguration<CostCenter> {
        public void Configure(EntityTypeBuilder<CostCenter> builder) {
            builder.ToTable("acc_cost_center");
            builder.Property(c => c.Code).IsRequired().HasMaxLength(20);
            builder.Property(c => c.CenterName).IsRequired().HasMaxLength(80);
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);
            builder.Property(c => c.BranchId).IsRequired(false).HasMaxLength(80);

            builder.HasOne(c => c.Branch)
                .WithMany(c => c.CostCenters)
                .HasForeignKey(y => y.BranchId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}


