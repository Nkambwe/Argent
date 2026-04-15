using Argent.Api.Domain.Entities.Support.KycLookup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Lookup.Configurations {
    public class IncomeTypeConfiguration : IEntityTypeConfiguration<IncomeType> {
        public void Configure(EntityTypeBuilder<IncomeType> builder) {
            builder.ToTable("kyc_income_types");
            builder.Property(i => i.Code).IsRequired().HasMaxLength(20);
            builder.Property(i => i.Name).IsRequired().HasMaxLength(100);
            builder.Property(i => i.Notes).HasMaxLength(500);
            builder.Property(i => i.CreatedBy).HasMaxLength(100);
            builder.Property(i => i.UpdatedBy).HasMaxLength(100);
            builder.Property(i => i.DeletedBy).HasMaxLength(100);
            builder.HasIndex(i => i.Code).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_income_types_code");
        }
    }

}
