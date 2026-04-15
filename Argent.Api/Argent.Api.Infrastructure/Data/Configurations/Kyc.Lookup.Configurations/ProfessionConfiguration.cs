using Argent.Api.Domain.Entities.Support.KycLookup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Lookup.Configurations {
    public class ProfessionConfiguration : IEntityTypeConfiguration<Profession> {
        public void Configure(EntityTypeBuilder<Profession> builder) {
            builder.ToTable("kyc_professions");
            builder.Property(p => p.Code).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.CreatedBy).HasMaxLength(100);
            builder.Property(p => p.UpdatedBy).HasMaxLength(100);
            builder.Property(p => p.DeletedBy).HasMaxLength(100);
            builder.HasIndex(p => p.Code).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_professions_code");
        }
    }

}
