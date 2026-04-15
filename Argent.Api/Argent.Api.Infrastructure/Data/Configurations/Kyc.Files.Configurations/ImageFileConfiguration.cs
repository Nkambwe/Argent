using Argent.Api.Domain.Entities.Kyc.KycFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Files.Configurations {
    public class ImageFileConfiguration : IEntityTypeConfiguration<ImageFile> {
        public void Configure(EntityTypeBuilder<ImageFile> builder) {
            builder.ToTable("kyc_image_files");
            builder.Property(i => i.FileUrl).IsRequired().HasMaxLength(500);
            builder.Property(i => i.Notes).HasMaxLength(300);
            builder.Property(i => i.CreatedBy).HasMaxLength(100);
            builder.Property(i => i.UpdatedBy).HasMaxLength(100);
            builder.Property(i => i.DeletedBy).HasMaxLength(100);

            builder.HasOne(i => i.OtherFile)
                .WithMany(f => f.Images)
                .HasForeignKey(i => i.OtherFileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.TitleDeed)
                .WithMany(t => t.Images)
                .HasForeignKey(i => i.TitleDeedId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Identification)
                .WithMany(id => id.Images)
                .HasForeignKey(i => i.IdentificationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
