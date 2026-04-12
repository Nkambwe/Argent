using Argent.Api.Domain.Entities.Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Access.Configuration {
    public class RoleGroupConfiguration : IEntityTypeConfiguration<RoleGroup> {
        public void Configure(EntityTypeBuilder<RoleGroup> builder) {
            builder.ToTable("role_groups");
            builder.Property(rg => rg.Name).IsRequired().HasMaxLength(100);
            builder.Property(rg => rg.Description).HasMaxLength(300);
            builder.Property(rg => rg.CreatedBy).HasMaxLength(100);
            builder.Property(rg => rg.UpdatedBy).HasMaxLength(100);
            builder.Property(rg => rg.DeletedBy).HasMaxLength(100);
            builder.HasIndex(rg => rg.Name).IsUnique().HasDatabaseName("ux_role_groups_name");
        }
    }
}
