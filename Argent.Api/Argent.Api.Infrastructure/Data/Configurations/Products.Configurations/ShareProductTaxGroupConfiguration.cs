using Argent.Api.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class ShareProductTaxGroupConfiguration: IEntityTypeConfiguration<ShareProductTaxGroup> {
        public void Configure(EntityTypeBuilder<ShareProductTaxGroup> b) {
            b.ToTable("prd_share_tax_groups");
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.ShareProductId, x.TaxGroupId }).IsUnique();
        }
    }

}
