using Argent.Api.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class InsuranceProductConfigurationEfConfig : IEntityTypeConfiguration<InsuranceProductConfiguration> {
        public void Configure(EntityTypeBuilder<InsuranceProductConfiguration> b) {
            b.ToTable("prd_insurance_configs");
            b.HasKey(x => x.Id);
            b.Property(x => x.MonthlyPremium).HasColumnType("decimal(18,2)");
            b.Property(x => x.PremiumPercentageCharged).HasColumnType("decimal(10,4)");
            b.Property(x => x.FixedAmount).HasColumnType("decimal(18,2)");
            b.Property(x => x.MinimumCoverage).HasColumnType("decimal(18,2)");
            b.Property(x => x.MaximumCoverage).HasColumnType("decimal(18,2)");
            b.Property(x => x.Discount).HasColumnType("decimal(10,4)");
            b.Property(x => x.ClaimsPercentage).HasColumnType("decimal(10,4)");
            b.Property(x => x.AdministrationCostPercentage).HasColumnType("decimal(10,4)");
            b.Property(x => x.AdministrationFundPercentage).HasColumnType("decimal(10,4)");
        }
    }

}
