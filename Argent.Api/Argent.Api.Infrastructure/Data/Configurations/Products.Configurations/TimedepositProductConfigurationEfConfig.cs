using Argent.Api.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class TimedepositProductConfigurationEfConfig
        : IEntityTypeConfiguration<TimedepositProductConfiguration> {
        public void Configure(EntityTypeBuilder<TimedepositProductConfiguration> b) {
            b.ToTable("prd_timedeposit_configs");
            b.HasKey(x => x.Id);
            b.Property(x => x.MinimumProductAmount).HasColumnType("decimal(18,2)");
            b.Property(x => x.MaximumProductAmount).HasColumnType("decimal(18,2)");
            b.Property(x => x.MinimumInterestRate).HasColumnType("decimal(10,4)");
            b.Property(x => x.MaximumInterestRate).HasColumnType("decimal(10,4)");
            b.Property(x => x.PenaltyAmount).HasColumnType("decimal(18,2)");
            b.Property(x => x.PenaltyRate).HasColumnType("decimal(10,4)");
        }
    }

}
