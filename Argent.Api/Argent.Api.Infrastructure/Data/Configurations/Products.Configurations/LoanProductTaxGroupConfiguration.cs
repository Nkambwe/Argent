using Argent.Api.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class LoanProductTaxGroupConfiguration
        : IEntityTypeConfiguration<LoanProductTaxGroup> {
        public void Configure(EntityTypeBuilder<LoanProductTaxGroup> b) {
            b.ToTable("prd_loan_tax_groups");
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.LoanProductId, x.TaxGroupId }).IsUnique();
        }
    }

}
