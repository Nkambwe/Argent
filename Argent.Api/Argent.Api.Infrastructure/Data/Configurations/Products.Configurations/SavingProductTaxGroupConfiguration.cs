using Argent.Api.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class SavingProductTaxGroupConfiguration : IEntityTypeConfiguration<SavingProductTaxGroup> {
        public void Configure(EntityTypeBuilder<SavingProductTaxGroup> b) {
            b.ToTable("prd_saving_tax_groups");
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.SavingProductId, x.TaxGroupId }).IsUnique();
        }
    }

}
