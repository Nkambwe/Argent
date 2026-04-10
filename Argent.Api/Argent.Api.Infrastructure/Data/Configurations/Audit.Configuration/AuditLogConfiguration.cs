using Argent.Api.Domain.Entities.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Audit.Configuration {
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog> {
        public void Configure(EntityTypeBuilder<AuditLog> builder) {
            builder.ToTable("audit_logs");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Username).HasMaxLength(100);
            builder.Property(a => a.BranchName).HasMaxLength(150);
            builder.Property(a => a.IpAddress).HasMaxLength(50);
            builder.Property(a => a.Module).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Action).IsRequired().HasMaxLength(150);
            builder.Property(a => a.EntityName).HasMaxLength(100);
            builder.Property(a => a.EntityId).HasMaxLength(100);
            builder.Property(a => a.ActionType).HasConversion<int>();
            builder.Property(a => a.OldValues).HasColumnType("text");
            builder.Property(a => a.NewValues).HasColumnType("text");
            builder.Property(a => a.FailureReason).HasMaxLength(500);
            builder.Ignore(a => a.UpdatedOn);
            builder.Ignore(a => a.UpdatedBy);
            builder.Ignore(a => a.IsDeleted);
            builder.Ignore(a => a.DeletedOn);
            builder.Ignore(a => a.DeletedBy);
            builder.HasIndex(a => a.UserId).HasDatabaseName("ix_audit_logs_user");
            builder.HasIndex(a => a.BranchId).HasDatabaseName("ix_audit_logs_branch");
            builder.HasIndex(a => a.OccurredOn).HasDatabaseName("ix_audit_logs_occurred_at");
            builder.HasIndex(a => new { a.EntityName, a.EntityId }).HasDatabaseName("ix_audit_logs_entity");
            builder.HasIndex(a => new { a.Module, a.Action }).HasDatabaseName("ix_audit_logs_module_action");
        }
    }

}
