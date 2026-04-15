using Argent.Api.Domain.Entities.Support.KycLookup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Lookup.Configurations {
    public class NationalityConfiguration : IEntityTypeConfiguration<Nationality> {
        public void Configure(EntityTypeBuilder<Nationality> builder) {
            builder.ToTable("kyc_nationalities");
            builder.Property(n => n.Code).IsRequired().HasMaxLength(10);
            builder.Property(n => n.Name).IsRequired().HasMaxLength(100);
            builder.Property(n => n.CreatedBy).HasMaxLength(100);
            builder.Property(n => n.UpdatedBy).HasMaxLength(100);
            builder.Property(n => n.DeletedBy).HasMaxLength(100);
            builder.HasIndex(n => n.Code).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_nationalities_code");
        }
    }

}
