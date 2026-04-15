using Argent.Api.Domain.Entities.Kyc.KycIndividuals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Configurations {
    public class GuarantorConfiguration : IEntityTypeConfiguration<Guarantor> {
        public void Configure(EntityTypeBuilder<Guarantor> builder) {
            builder.ToTable("kyc_guarantors");
            builder.Property(g => g.Code).IsRequired().HasMaxLength(50);
            builder.Property(g => g.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(g => g.MiddleName).HasMaxLength(100);
            builder.Property(g => g.LastName).IsRequired().HasMaxLength(100);
            builder.Property(g => g.Photo).HasMaxLength(500);
            builder.Property(g => g.Signature).HasMaxLength(500);
            builder.Property(g => g.PermanentAddress).HasMaxLength(300);
            builder.Property(g => g.Telephone).HasMaxLength(30);
            builder.Property(g => g.Mobile).HasMaxLength(30);
            builder.Property(g => g.Email).HasMaxLength(150);
            builder.Property(g => g.City).HasMaxLength(100);
            builder.Property(g => g.Town).HasMaxLength(100);
            builder.Property(g => g.CreatedBy).HasMaxLength(100);
            builder.Property(g => g.UpdatedBy).HasMaxLength(100);
            builder.Property(g => g.DeletedBy).HasMaxLength(100);

            builder.HasOne(g => g.Title).WithMany()
                .HasForeignKey(g => g.TitleId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(g => g.Nationality).WithMany()
                .HasForeignKey(g => g.NationalityId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(g => g.Village).WithMany()
                .HasForeignKey(g => g.VillageId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(g => g.Profession).WithMany()
                .HasForeignKey(g => g.ProfessionId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
