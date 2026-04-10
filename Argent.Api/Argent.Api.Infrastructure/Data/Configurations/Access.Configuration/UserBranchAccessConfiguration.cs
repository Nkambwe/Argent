using Argent.Api.Domain.Entities.Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Access.Configuration {
    public class UserBranchAccessConfiguration : IEntityTypeConfiguration<UserBranchAccess> {
        public void Configure(EntityTypeBuilder<UserBranchAccess> builder) {
            builder.ToTable("user_branch_access");
            builder.HasKey(ub => ub.Id);
            builder.Property(ub => ub.CanPost).HasDefaultValue(true);
            builder.HasIndex(ub => new { ub.UserId, ub.BranchId }).IsUnique().HasDatabaseName("ux_user_branch_access_user_branch");
            builder.HasIndex(ub => ub.UserId).HasDatabaseName("ix_user_branch_access_user");
            builder.HasIndex(ub => ub.BranchId).HasDatabaseName("ix_user_branch_access_branch");
            builder.HasOne(ub => ub.User).WithMany(u => u.BranchAccess).HasForeignKey(ub => ub.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(ub => ub.Branch).WithMany().HasForeignKey(ub => ub.BranchId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
