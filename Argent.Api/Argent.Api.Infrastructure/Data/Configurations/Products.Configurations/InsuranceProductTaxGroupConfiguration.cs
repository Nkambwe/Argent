using Argent.Api.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class InsuranceProductTaxGroupConfiguration : IEntityTypeConfiguration<InsuranceProductTaxGroup> {
        public void Configure(EntityTypeBuilder<InsuranceProductTaxGroup> b) {
            b.ToTable("prd_insurance_tax_groups");
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.InsuranceProductId, x.TaxGroupId }).IsUnique();
        }
    }

}
