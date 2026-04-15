using Argent.Api.Domain.Entities.Support.KycLookup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Lookup.Configurations {
    public class VillageConfiguration : IEntityTypeConfiguration<Village> {
        public void Configure(EntityTypeBuilder<Village> builder) {
            builder.ToTable("kyc_villages");
            builder.Property(v => v.Code).IsRequired().HasMaxLength(20);
            builder.Property(v => v.Name).IsRequired().HasMaxLength(150);
            builder.Property(v => v.Parish).HasMaxLength(100);
            builder.Property(v => v.SubCounty).HasMaxLength(100);
            builder.Property(v => v.County).HasMaxLength(100);
            builder.Property(v => v.District).HasMaxLength(100);
            builder.Property(v => v.Region).HasMaxLength(100);
            builder.Property(v => v.CreatedBy).HasMaxLength(100);
            builder.Property(v => v.UpdatedBy).HasMaxLength(100);
            builder.Property(v => v.DeletedBy).HasMaxLength(100);
        }
    }

}
