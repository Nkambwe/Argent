using Argent.Api.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Settings.Configurations {
    public class SystemPolicyConfiguration : IEntityTypeConfiguration<SystemPolicy> {
        public void Configure(EntityTypeBuilder<SystemPolicy> builder) {
            builder.ToTable("system_policies");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Module).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(300);
            builder.Property(p => p.DefaultValue).IsRequired().HasMaxLength(500);
            builder.Property(p => p.CreatedBy).HasMaxLength(100);
            builder.Property(p => p.UpdatedBy).HasMaxLength(100);
            builder.Property(p => p.DeletedBy).HasMaxLength(100);

            builder.HasIndex(p => new { p.Module, p.Name }).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_system_policies_module_name");
        }
    }
}
