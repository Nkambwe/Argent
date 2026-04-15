using Argent.Api.Domain.Entities.Support.KycLookup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Files.Configurations {
    public class IdentificationTypeConfiguration : IEntityTypeConfiguration<IdentificationType> {
        public void Configure(EntityTypeBuilder<IdentificationType> builder) {
            builder.ToTable("kyc_identification_types");
            builder.Property(i => i.TypeName).IsRequired().HasMaxLength(100);
            builder.Property(i => i.LocalFolder).HasMaxLength(300);
            builder.Property(i => i.FtpFolder).HasMaxLength(300);
            builder.Property(i => i.Notes).HasMaxLength(500);
            builder.Property(i => i.CreatedBy).HasMaxLength(100);
            builder.Property(i => i.UpdatedBy).HasMaxLength(100);
            builder.Property(i => i.DeletedBy).HasMaxLength(100);
        }
    }

}
