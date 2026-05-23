using Argent.Api.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class TimedepositInterestTierConfiguration : IEntityTypeConfiguration<TimedepositInterestTier> {
        public void Configure(EntityTypeBuilder<TimedepositInterestTier> b) {
            b.ToTable("prd_timedeposit_tiers");
            b.HasKey(x => x.Id);
            b.Property(x => x.FromAmount).HasColumnType("decimal(18,2)");
            b.Property(x => x.ToAmount).HasColumnType("decimal(18,2)");
            b.Property(x => x.Rate).HasColumnType("decimal(10,4)");
        }
    }

}
