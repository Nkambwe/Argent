using Argent.Api.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Settings.Configurations {
    public class RoleGroupPolicyOverrideConfiguration : IEntityTypeConfiguration<RoleGroupPolicyOverride> {
        public void Configure(EntityTypeBuilder<RoleGroupPolicyOverride> builder) {
            builder.ToTable("role_group_policy_overrides");
            builder.Property(o => o.OverrideValue).IsRequired().HasMaxLength(500);
            builder.Property(o => o.Reason).HasMaxLength(300);
            builder.Property(o => o.CreatedBy).HasMaxLength(100);
            builder.Property(o => o.UpdatedBy).HasMaxLength(100);
            builder.Property(o => o.DeletedBy).HasMaxLength(100);
            builder.HasIndex(o => new { o.RoleGroupId, o.SystemPolicyId }).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_role_group_policy_overrides_group_policy");
            builder.HasOne(o => o.SystemPolicy).WithMany(p => p.Overrides).HasForeignKey(o => o.SystemPolicyId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
