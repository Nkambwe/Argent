using Argent.Api.Domain.Entities.Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Access.Configuration {
    public class RoleGroupConfiguration : IEntityTypeConfiguration<RoleGroup> {
        public void Configure(EntityTypeBuilder<RoleGroup> builder) {
            builder.ToTable("role_group");
            builder.HasKey(rb => rb.Id);
            builder.Property(rb => rb.Name).IsRequired().HasMaxLength(200);
            builder.Property(rb => rb.Description).HasMaxLength(300);
            builder.Property(rb => rb.CreatedBy).HasMaxLength(100);
            builder.Property(rb => rb.UpdatedBy).HasMaxLength(100);
            builder.Property(rb => rb.DeletedBy).HasMaxLength(100);
            builder.HasMany(rb => rb.Roles).WithOne(r => r.RoleGroup).HasForeignKey(r => r.RoleGroupId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
