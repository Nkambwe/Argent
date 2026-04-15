using Argent.Api.Domain.Entities.Kyc.KycFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Files.Configurations {
    public class OtherFileConfiguration : IEntityTypeConfiguration<OtherFile> {
        public void Configure(EntityTypeBuilder<OtherFile> builder) {
            builder.ToTable("kyc_other_files");
            builder.HasOne(f => f.Signatory)
                .WithMany(s => s.Files)
                .HasForeignKey(f => f.SignatoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

}
