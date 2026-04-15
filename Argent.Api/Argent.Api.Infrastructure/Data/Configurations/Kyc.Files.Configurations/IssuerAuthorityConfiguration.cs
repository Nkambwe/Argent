using Argent.Api.Domain.Entities.Support.KycLookup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Files.Configurations {
    public class IssuerAuthorityConfiguration : IEntityTypeConfiguration<IssuerAuthority> {
        public void Configure(EntityTypeBuilder<IssuerAuthority> builder) {
            builder.ToTable("kyc_issuer_authorities");
            builder.Property(a => a.Code).IsRequired().HasMaxLength(20);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(200);
            builder.Property(a => a.Notes).HasMaxLength(500);
            builder.Property(a => a.CreatedBy).HasMaxLength(100);
            builder.Property(a => a.UpdatedBy).HasMaxLength(100);
            builder.Property(a => a.DeletedBy).HasMaxLength(100);
        }
    }

}
