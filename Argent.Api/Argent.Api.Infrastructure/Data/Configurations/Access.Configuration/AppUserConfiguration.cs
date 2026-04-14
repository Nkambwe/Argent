using Argent.Api.Domain.Entities.Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Access.Configuration {

    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser> {

        public void Configure(EntityTypeBuilder<AppUser> builder) {
            builder.ToTable("users");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Username).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(150);
            builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(512);
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.MiddleName).IsRequired(false).HasMaxLength(100);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.PhoneNumber).HasMaxLength(20);
            builder.Property(u => u.CreatedBy).HasMaxLength(100);
            builder.Property(u => u.UpdatedBy).HasMaxLength(100);
            builder.Property(u => u.DeletedBy).HasMaxLength(100);
            builder.Property(u => u.IsActive).HasDefaultValue(true);
            builder.Property(u => u.FailedLoginAttempts).HasDefaultValue(0);
            builder.HasIndex(u => u.Username).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_users_username");
            builder.HasIndex(u => u.Email).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_users_email");
            builder.HasIndex(u => u.DefaultBranchId).HasDatabaseName("ix_users_home_branch");
            builder.HasOne(u => u.DefaultBranch).WithMany().HasForeignKey(u => u.DefaultBranchId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(u => u.UserRoles).WithOne(ur => ur.User).HasForeignKey(ur => ur.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.BranchAccess).WithOne(ba => ba.User).HasForeignKey(ba => ba.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.RefreshTokens).WithOne(rt => rt.User).HasForeignKey(rt => rt.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
