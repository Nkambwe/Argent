using Argent.Api.Domain.Entities.Support.KycLookup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Lookup.Configurations {
    public class EducationConfiguration : IEntityTypeConfiguration<Education> {
        public void Configure(EntityTypeBuilder<Education> builder) {
            builder.ToTable("kyc_education_levels");
            builder.Property(e => e.Code).IsRequired().HasMaxLength(20);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.CreatedBy).HasMaxLength(100);
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);
            builder.Property(e => e.DeletedBy).HasMaxLength(100);
            builder.HasIndex(e => e.Code).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_education_code");
        }
    }

}
