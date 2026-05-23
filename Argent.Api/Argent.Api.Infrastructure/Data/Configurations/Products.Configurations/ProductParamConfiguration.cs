using Argent.Api.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class ProductParamConfiguration : IEntityTypeConfiguration<ProductParam> {
        public void Configure(EntityTypeBuilder<ProductParam> b) {
            b.ToTable("prd_params");
            b.HasKey(x => x.Id);
            b.Property(x => x.ProductId).IsRequired();
            b.Property(x => x.ProductModule).IsRequired();
            b.Property(x => x.ParameterName).HasMaxLength(100).IsRequired();
            b.Property(x => x.ParamValue).HasMaxLength(500).IsRequired();
            b.Property(x => x.DataType).HasMaxLength(20).IsRequired();
            b.Property(x => x.Description).HasMaxLength(300);

            b.HasIndex(x => new { x.ProductId, x.ProductModule, x.ParameterName }).IsUnique();
        }
    }

}
