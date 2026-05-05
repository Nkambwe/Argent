using Argent.Api.Domain.Entities.Accounting.Charges;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class ChargeGroupConfiguration : IEntityTypeConfiguration<ChargeGroup> {
        public void Configure(EntityTypeBuilder<ChargeGroup> builder) {
            builder.ToTable("acc_charge_groups");
            builder.Property(g => g.SeriesIdentifier).IsRequired().HasMaxLength(20);
            builder.Property(g => g.SeriesPrefix).HasMaxLength(20);
            builder.Property(g => g.GroupName).IsRequired().HasMaxLength(150);
            builder.Property(g => g.Notes).HasMaxLength(500);
            builder.Property(g => g.CreatedBy).HasMaxLength(100);
            builder.Property(g => g.UpdatedBy).HasMaxLength(100);
            builder.Property(g => g.DeletedBy).HasMaxLength(100);
            builder.HasIndex(g => g.GroupName).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_charge_groups_name");
        }
    }
}
