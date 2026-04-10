using Argent.Api.Domain.Entities.Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Access.Configuration {
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken> {
        public void Configure(EntityTypeBuilder<RefreshToken> builder) {
            builder.ToTable("refresh_tokens");
            builder.HasKey(rt => rt.Id);
            builder.Property(rt => rt.Token).IsRequired().HasMaxLength(512);
            builder.Property(rt => rt.ReplacedByToken).HasMaxLength(512);
            builder.Property(rt => rt.CreatedByIp).HasMaxLength(50);
            builder.HasIndex(rt => rt.Token).HasDatabaseName("ix_refresh_tokens_token");
            builder.HasIndex(rt => rt.UserId).HasDatabaseName("ix_refresh_tokens_user");
            builder.HasOne(rt => rt.User).WithMany(u => u.RefreshTokens).HasForeignKey(rt => rt.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
