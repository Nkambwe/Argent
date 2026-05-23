using Argent.Api.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class ShareProductConfigurationEfConfig: IEntityTypeConfiguration<ShareProductConfiguration> {
        public void Configure(EntityTypeBuilder<ShareProductConfiguration> b) {
            b.ToTable("prd_share_configs");
            b.HasKey(x => x.Id);
            b.Property(x => x.NominalValue).HasColumnType("decimal(18,4)");
            b.Property(x => x.MinimumShareCapital).HasColumnType("decimal(18,2)");
            b.Property(x => x.DividendRate).HasColumnType("decimal(10,4)");
            b.Property(x => x.DividendEarningShares).HasMaxLength(100);
        }
    }

}
