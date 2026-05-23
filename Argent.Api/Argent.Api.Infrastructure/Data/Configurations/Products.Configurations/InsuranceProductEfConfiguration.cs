using Argent.Api.Domain.Entities.Products;
using Argent.Api.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class InsuranceProductEfConfiguration: IEntityTypeConfiguration<InsuranceProduct> {
        public void Configure(EntityTypeBuilder<InsuranceProduct> b) {
            b.ToTable("prd_insurance_products");
            b.HasKey(x => x.Id);
            b.Property(x => x.Code).HasMaxLength(20).IsRequired();
            b.Property(x => x.ProductName).HasMaxLength(100).IsRequired();
            b.Property(x => x.Description).HasMaxLength(300);
            b.HasIndex(x => x.Code).IsUnique();

            b.HasOne(x => x.ProductType)
                .WithMany(x => x.InsuranceProducts)
                .HasForeignKey(x => x.ProductTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.ChargeGroup)
                .WithMany()
                .HasForeignKey(x => x.ChargeGroupId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(x => x.Configuration)
                .WithOne(x => x.InsuranceProduct)
                .HasForeignKey<InsuranceProductConfiguration>(x => x.InsuranceProductId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.TaxGroups)
                .WithOne(x => x.InsuranceProduct)
                .HasForeignKey(x => x.InsuranceProductId)
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
