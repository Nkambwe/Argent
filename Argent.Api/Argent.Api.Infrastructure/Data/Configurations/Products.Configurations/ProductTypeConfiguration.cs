using Argent.Api.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType> {
        public void Configure(EntityTypeBuilder<ProductType> b) {
            b.ToTable("prd_product_types");
            b.HasKey(x => x.Id);
            b.Property(x => x.Code).HasMaxLength(20).IsRequired();
            b.Property(x => x.Name).HasMaxLength(100).IsRequired();
            b.Property(x => x.Description).HasMaxLength(300);
            b.Property(x => x.Module).IsRequired();
            b.HasIndex(x => x.Code).IsUnique();
        }
    }

}
