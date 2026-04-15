using Argent.Api.Domain.Entities.Kyc.KycFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Files.Configurations {
    public class FileAttachmentConfiguration : IEntityTypeConfiguration<FileAttachment> {
        public void Configure(EntityTypeBuilder<FileAttachment> builder) {
            builder.ToTable("kyc_file_attachments");
            builder.UseTptMappingStrategy();
            builder.Property(f => f.Series).HasMaxLength(20);
            builder.Property(f => f.FileUrl).IsRequired().HasMaxLength(500);
            builder.Property(f => f.Notes).HasMaxLength(500);
            builder.Property(f => f.CreatedBy).HasMaxLength(100);
            builder.Property(f => f.UpdatedBy).HasMaxLength(100);
            builder.Property(f => f.DeletedBy).HasMaxLength(100);
            builder.HasIndex(f => new { f.CustomerId, f.CustomerType })
                .HasDatabaseName("ix_file_attachments_customer");
        }
    }

}
