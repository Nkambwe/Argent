using Argent.Api.Domain.Entities.Support.KycLookup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Lookup.Configurations {
    public class GroupPositionConfiguration : IEntityTypeConfiguration<GroupPosition> {
        public void Configure(EntityTypeBuilder<GroupPosition> builder) {
            builder.ToTable("kyc_group_positions");
            builder.Property(p => p.Series).HasMaxLength(20);
            builder.Property(p => p.Designation).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Notes).HasMaxLength(500);
            builder.Property(p => p.CreatedBy).HasMaxLength(100);
            builder.Property(p => p.UpdatedBy).HasMaxLength(100);
            builder.Property(p => p.DeletedBy).HasMaxLength(100);
        }
    }

}
