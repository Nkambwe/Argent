using Argent.Api.Domain.Entities.Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Access.Configuration {
    public partial class UserBranchAccessConfiguration {
        public class PasswordHistoryConfiguration : IEntityTypeConfiguration<PasswordHistory> {
            public void Configure(EntityTypeBuilder<PasswordHistory> builder) {
                builder.ToTable("password_history");
                builder.Property(ph => ph.PasswordHash).IsRequired().HasMaxLength(500);
                builder.Property(ph => ph.CreatedBy).HasMaxLength(100);
                builder.Property(ph => ph.UpdatedBy).HasMaxLength(100);
                builder.Property(ph => ph.DeletedBy).HasMaxLength(100);
                builder.HasIndex(ph => new { ph.UserId, ph.ChangedOn }).HasDatabaseName("ix_password_history_user_changed");
                builder.HasOne(ph => ph.User).WithMany(u => u.PasswordHistory).HasForeignKey(ph => ph.UserId).OnDelete(DeleteBehavior.Cascade);
            }
        }
    }
}
