using Argent.Api.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class TimedepositRateConfiguration : IEntityTypeConfiguration<TimedepositRate> {
        public void Configure(EntityTypeBuilder<TimedepositRate> b) {
            b.ToTable("prd_timedeposit_rates");
            b.HasKey(x => x.Id);
            b.Property(x => x.InterestRate).HasColumnType("decimal(10,4)");
            b.Property(x => x.MinimumAmount).HasColumnType("decimal(18,2)");
        }
    }

}
