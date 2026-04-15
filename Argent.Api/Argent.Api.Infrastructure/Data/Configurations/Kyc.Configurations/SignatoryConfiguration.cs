using Argent.Api.Domain.Entities.Kyc.KycBusinesses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Configurations {
    public class SignatoryConfiguration : IEntityTypeConfiguration<Signatory> {
        public SignatoryConfiguration() {
        }

        public void Configure(EntityTypeBuilder<Signatory> builder) {
            builder.ToTable("kyc_signatories");
            builder.Property(s => s.Code).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(200);
            builder.Property(s => s.Photo).HasMaxLength(500);
            builder.Property(s => s.Signature).HasMaxLength(500);
            builder.Property(s => s.Telephone).HasMaxLength(30);
            builder.Property(s => s.Mobile).HasMaxLength(30);
            builder.Property(s => s.Email).HasMaxLength(150);
            builder.Property(s => s.Position).HasMaxLength(100);
            builder.Property(s => s.PassCode).HasMaxLength(100);
            builder.Property(s => s.Notes).HasMaxLength(500);
            builder.Property(s => s.CreatedBy).HasMaxLength(100);
            builder.Property(s => s.UpdatedBy).HasMaxLength(100);
            builder.Property(s => s.DeletedBy).HasMaxLength(100);

            builder.HasOne(s => s.Business)
                .WithMany(b => b.Signatories)
                .HasForeignKey(s => s.BusinessId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
