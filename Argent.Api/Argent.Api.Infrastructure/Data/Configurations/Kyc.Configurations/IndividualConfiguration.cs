using Argent.Api.Domain.Entities.Kyc.KycIndividuals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Configurations {
    public class IndividualConfiguration : IEntityTypeConfiguration<Individual> {
        public void Configure(EntityTypeBuilder<Individual> builder) {
            builder.ToTable("customers_individuals");
            builder.Property(i => i.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(i => i.MiddleName).HasMaxLength(100);
            builder.Property(i => i.LastName).IsRequired().HasMaxLength(100);
            builder.Property(i => i.BirthPlace).HasMaxLength(150);
            builder.Property(i => i.SpouseName).HasMaxLength(150);
            builder.Property(i => i.Mother).HasMaxLength(150);
            builder.Property(i => i.Father).HasMaxLength(150);
            builder.Property(i => i.Photo).HasMaxLength(500);
            builder.Property(i => i.Signature).HasMaxLength(500);
            builder.Property(i => i.RightThumbPrint).HasMaxLength(500);
            builder.Property(i => i.LeftThumbPrint).HasMaxLength(500);

            builder.HasOne(i => i.Title)
                .WithMany().HasForeignKey(i => i.TitleId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(i => i.Nationality)
                .WithMany().HasForeignKey(i => i.NationalityId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(i => i.Profession)
                .WithMany().HasForeignKey(i => i.ProfessionId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(i => i.Education)
                .WithMany().HasForeignKey(i => i.EducationId).OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(i => new { i.LastName, i.FirstName })
                .HasDatabaseName("ix_individuals_name");
        }
    }
}
