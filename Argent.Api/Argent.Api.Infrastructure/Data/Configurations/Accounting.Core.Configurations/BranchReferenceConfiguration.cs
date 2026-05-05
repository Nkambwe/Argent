using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Core.Configurations {
    public class BranchReferenceConfiguration : IEntityTypeConfiguration<BranchReference> {
        public void Configure(EntityTypeBuilder<BranchReference> builder) {
            builder.ToTable("branch_references");
            builder.Property(r => r.Series).HasMaxLength(20);
            builder.Property(r => r.Description).IsRequired().HasMaxLength(150);
            builder.Property(r => r.Suspend).HasDefaultValue(false);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);
        }
    }
}
