using Argent.Api.Domain.Entities.Products;
using Argent.Api.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class SavingProductConfig : IEntityTypeConfiguration<SavingProduct> {
        public void Configure(EntityTypeBuilder<SavingProduct> b) {
            b.ToTable("prd_saving_products");
            b.HasKey(x => x.Id);
            b.Property(x => x.Code).HasMaxLength(20).IsRequired();
            b.Property(x => x.ProductName).HasMaxLength(100).IsRequired();
            b.Property(x => x.Description).HasMaxLength(300);
            b.Property(x => x.InterestRate).HasColumnType("decimal(10,4)");
            b.Property(x => x.OverdraftInterest).HasColumnType("decimal(10,4)");
            b.Property(x => x.MinimumBalance).HasColumnType("decimal(18,2)");
            b.Property(x => x.WithdrawPenalty).HasColumnType("decimal(18,2)");
            b.Property(x => x.MinimumInterestOffered).HasColumnType("decimal(18,2)");
            b.HasIndex(x => x.Code).IsUnique();

            b.HasOne(x => x.ProductType)
                .WithMany(x => x.SavingProducts)
                .HasForeignKey(x => x.ProductTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.ChargeGroup)
                .WithMany()
                .HasForeignKey(x => x.ChargeGroupId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(x => x.Configuration)
                .WithOne(x => x.SavingProduct)
                .HasForeignKey<SavingProductConfiguration>(x => x.SavingProductId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.TaxGroups)
                .WithOne(x => x.SavingProduct)
                .HasForeignKey(x => x.SavingProductId)
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
