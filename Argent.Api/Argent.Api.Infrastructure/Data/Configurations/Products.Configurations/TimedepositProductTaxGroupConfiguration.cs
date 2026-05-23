using Argent.Api.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class TimedepositProductTaxGroupConfiguration : IEntityTypeConfiguration<TimedepositProductTaxGroup> {
        public void Configure(EntityTypeBuilder<TimedepositProductTaxGroup> b) {
            b.ToTable("prd_timedeposit_tax_groups");
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.TimedepositProductId, x.TaxGroupId }).IsUnique();
        }
    }

}
