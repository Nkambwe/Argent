using Argent.Api.Domain.Entities.Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Access.Configuration {
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission> {
        public void Configure(EntityTypeBuilder<RolePermission> builder) {
            builder.ToTable("role_permissions");
            builder.Property(rp => rp.CreatedBy).HasMaxLength(100);
            builder.Property(rp => rp.UpdatedBy).HasMaxLength(100);
            builder.Property(rp => rp.DeletedBy).HasMaxLength(100);
            builder.HasIndex(rp => new { rp.RoleId, rp.PermissionId }).IsUnique().HasDatabaseName("ux_role_permissions_role_permission");
            builder.HasOne(rp => rp.Permission).WithMany(p => p.RolePermissions).HasForeignKey(rp => rp.PermissionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
