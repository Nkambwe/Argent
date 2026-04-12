using Argent.Api.Domain.Entities.Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Access.Configuration {

    public class PermissionConfiguration : IEntityTypeConfiguration<Permission> {

        public void Configure(EntityTypeBuilder<Permission> builder) {
            builder.ToTable("permissions");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Module).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Action).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(300);
            builder.Property(p => p.CreatedBy).HasMaxLength(100);
            builder.Property(p => p.UpdatedBy).HasMaxLength(100);
            builder.Property(p => p.DeletedBy).HasMaxLength(100);
            builder.HasIndex(p => p.Name).IsUnique().HasDatabaseName("ux_permissions_name");
            builder.HasIndex(p => new { p.Module, p.Action }).HasDatabaseName("ix_permissions_module_action");
        }
    }
}
