using Argent.Api.Domain.Entities.Products;
using Argent.Api.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class TimedepositProductEfConfiguration : IEntityTypeConfiguration<TimedepositProduct> {
        public void Configure(EntityTypeBuilder<TimedepositProduct> b) {
            b.ToTable("prd_timedeposit_products");
            b.HasKey(x => x.Id);
            b.Property(x => x.Code).HasMaxLength(20).IsRequired();
            b.Property(x => x.ProductName).HasMaxLength(100).IsRequired();
            b.Property(x => x.Description).HasMaxLength(300);
            b.Property(x => x.MinimumAmount).HasColumnType("decimal(18,2)");
            b.Property(x => x.MaximumAmount).HasColumnType("decimal(18,2)");
            b.Property(x => x.PrematureWithdrawPenalty).HasColumnType("decimal(10,4)");
            b.HasIndex(x => x.Code).IsUnique();

            b.HasOne(x => x.ProductType)
                .WithMany(x => x.TimedepositProducts)
                .HasForeignKey(x => x.ProductTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.ChargeGroup)
                .WithMany()
                .HasForeignKey(x => x.ChargeGroupId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(x => x.Configuration)
                .WithOne(x => x.TimedepositProduct)
                .HasForeignKey<TimedepositProductConfiguration>(x => x.TimedepositProductId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.InterestRates)
                .WithOne(x => x.TimedepositProduct)
                .HasForeignKey(x => x.TimedepositProductId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.InterestTiers)
                .WithOne(x => x.TimedepositProduct)
                .HasForeignKey(x => x.TimedepositProductId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.TaxGroups)
                .WithOne(x => x.TimedepositProduct)
                .HasForeignKey(x => x.TimedepositProductId)
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
