using Argent.Api.Domain.Entities.Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Access.Configuration {
    public class RoleGroupMemberConfiguration : IEntityTypeConfiguration<RoleGroupMember> {
        public void Configure(EntityTypeBuilder<RoleGroupMember> builder) {
            builder.ToTable("role_group_members");
            builder.Property(m => m.CreatedBy).HasMaxLength(100);
            builder.Property(m => m.UpdatedBy).HasMaxLength(100);
            builder.Property(m => m.DeletedBy).HasMaxLength(100);
            builder.HasIndex(m => new { m.RoleGroupId, m.RoleId }).IsUnique().HasDatabaseName("ux_role_group_members_group_role");
            builder.HasOne(m => m.RoleGroup).WithMany(rg => rg.Members).HasForeignKey(m => m.RoleGroupId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.Role).WithMany(r => r.RoleGroupMembers).HasForeignKey(m => m.RoleId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
