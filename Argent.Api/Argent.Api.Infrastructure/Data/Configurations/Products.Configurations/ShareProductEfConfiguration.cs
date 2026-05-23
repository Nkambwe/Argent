using Argent.Api.Domain.Entities.Products;
using Argent.Api.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class ShareProductEfConfiguration : IEntityTypeConfiguration<ShareProduct> {
        public void Configure(EntityTypeBuilder<ShareProduct> b) {
            b.ToTable("prd_share_products");
            b.HasKey(x => x.Id);
            b.Property(x => x.Code).HasMaxLength(20).IsRequired();
            b.Property(x => x.ProductName).HasMaxLength(100).IsRequired();
            b.Property(x => x.Description).HasMaxLength(300);
            b.HasIndex(x => x.Code).IsUnique();

            b.HasOne(x => x.ProductType)
                .WithMany(x => x.ShareProducts)
                .HasForeignKey(x => x.ProductTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.ChargeGroup)
                .WithMany()
                .HasForeignKey(x => x.ChargeGroupId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(x => x.Configuration)
                .WithOne(x => x.ShareProduct)
                .HasForeignKey<ShareProductConfiguration>(x => x.ShareProductId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.TaxGroups)
                .WithOne(x => x.ShareProduct)
                .HasForeignKey(x => x.ShareProductId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.Params)
                .WithOne()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.PostingAccounts)
                .WithOne()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
