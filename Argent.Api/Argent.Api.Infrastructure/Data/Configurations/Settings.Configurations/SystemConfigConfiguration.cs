using Argent.Api.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Settings.Configurations {
    public class SystemConfigConfiguration : IEntityTypeConfiguration<SystemConfiguration> {
        public void Configure(EntityTypeBuilder<SystemConfiguration> builder) {
            builder.ToTable("system_configs");
            builder.Property(c => c.Module).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Key).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Value).IsRequired().HasMaxLength(500);
            builder.Property(c => c.Description).HasMaxLength(300);
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);
            builder.HasIndex(c => new { c.Module, c.Key }).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_system_configs_module_key");
        }
    }
}
