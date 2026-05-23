using Argent.Api.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class ProductPostingAccountConfiguration : IEntityTypeConfiguration<ProductPostingAccount> {
        public void Configure(EntityTypeBuilder<ProductPostingAccount> b) {
            b.ToTable("prd_posting_accounts");
            b.HasKey(x => x.Id);
            b.Property(x => x.ProductId).IsRequired();
            b.Property(x => x.ProductModule).IsRequired();
            b.Property(x => x.PostingPurpose).IsRequired();
            b.Property(x => x.CustomerSegment).IsRequired();
            b.Property(x => x.LedgerNumber).HasMaxLength(20).IsRequired();
            b.Property(x => x.CostCentreCode).HasMaxLength(30);
            b.Property(x => x.RevenueCentreCode).HasMaxLength(30);
            b.HasIndex(x => new
            {
                x.ProductId,
                x.ProductModule,
                x.PostingPurpose,
                x.CustomerSegment
            }).IsUnique();
        }
    }

}
