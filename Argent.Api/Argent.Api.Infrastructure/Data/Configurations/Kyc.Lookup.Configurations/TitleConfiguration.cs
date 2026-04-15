using Argent.Api.Domain.Entities.Support.KycLookup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Lookup.Configurations {
    public class TitleConfiguration : IEntityTypeConfiguration<Title> {
        public void Configure(EntityTypeBuilder<Title> builder) {
            builder.ToTable("kyc_titles");
            builder.Property(t => t.Name).IsRequired().HasMaxLength(50);
            builder.Property(t => t.CreatedBy).HasMaxLength(100);
            builder.Property(t => t.UpdatedBy).HasMaxLength(100);
            builder.Property(t => t.DeletedBy).HasMaxLength(100);
            builder.HasIndex(t => t.Name).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_titles_name");
        }
    }

}
