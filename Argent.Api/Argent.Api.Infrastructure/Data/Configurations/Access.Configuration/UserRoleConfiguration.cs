using Argent.Api.Domain.Entities.Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Access.Configuration {
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole> {
        public void Configure(EntityTypeBuilder<UserRole> builder) {
            builder.ToTable("user_roles");
            builder.Property(ur => ur.CreatedBy).HasMaxLength(100);
            builder.Property(ur => ur.UpdatedBy).HasMaxLength(100);
            builder.Property(ur => ur.DeletedBy).HasMaxLength(100);
            builder.HasIndex(ur => new { ur.UserId, ur.RoleId }).IsUnique().HasDatabaseName("ux_user_roles_user_role");
            builder.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId).OnDelete(DeleteBehavior.Restrict);
        }
    }

}
