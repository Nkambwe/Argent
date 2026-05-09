using Argent.Api.Domain.Entities.Accounting.Taxes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {

    public class TaxableItemConfiguration : IEntityTypeConfiguration<TaxableItem> {
        public TaxableItemConfiguration() {
        }

        public void Configure(EntityTypeBuilder<TaxableItem> builder) {
            builder.ToTable("acc_tax_items");
            builder.Property(r => r.ItemCode).IsRequired().HasMaxLength(20);
            builder.Property(r => r.Item).IsRequired().HasMaxLength(150);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);
            builder.HasIndex(r => r.ItemCode).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_tax_items_code");

            builder.HasOne(r => r.Tax)
                .WithMany(h => h.TaxableItems)
                .HasForeignKey(h => h.TaxId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.InsuranceProduct)
                .WithMany(h => h.TaxableItems)
                .HasForeignKey(h => h.TimedepositProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.TimedepositProduct)
                .WithMany(h => h.TaxableItems)
                .HasForeignKey(h => h.InsuranceProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.ShareProduct)
                .WithMany(h => h.TaxableItems)
                .HasForeignKey(h => h.ShareProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.SavingProduct)
                .WithMany(h => h.TaxableItems)
                .HasForeignKey(h => h.SavingProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.LoanProduct)
                .WithMany(h => h.TaxableItems)
                .HasForeignKey(h => h.LoanProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }

}
