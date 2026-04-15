using Argent.Api.Domain.Entities.Kyc.KycFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Files.Configurations {
    public class IdentificationConfiguration : IEntityTypeConfiguration<Identification> {
        public void Configure(EntityTypeBuilder<Identification> builder) {
            builder.ToTable("kyc_identifications");
            builder.Property(i => i.FileUrl).HasMaxLength(500);
            builder.Property(i => i.Notes).HasMaxLength(500);
            builder.Property(i => i.CreatedBy).HasMaxLength(100);
            builder.Property(i => i.UpdatedBy).HasMaxLength(100);
            builder.Property(i => i.DeletedBy).HasMaxLength(100);

            builder.HasOne(i => i.IdentityType)
                .WithMany()
                .HasForeignKey(i => i.IdentityTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.IssuerAuthority)
                .WithMany()
                .HasForeignKey(i => i.IssuerAuthorityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Signatory)
                .WithMany(s => s.Identifications)
                .HasForeignKey(i => i.SignatoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(i => i.Guarantor)
                .WithMany(g => g.Identifications)
                .HasForeignKey(i => i.GuarantorId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(i => new { i.CustomerId, i.CustomerType })
                .HasDatabaseName("ix_identifications_customer");
        }
    }

}
