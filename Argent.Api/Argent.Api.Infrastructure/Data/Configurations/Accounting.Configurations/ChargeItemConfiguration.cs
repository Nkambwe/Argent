using Argent.Api.Domain.Entities.Accounting.Charges;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class ChargeItemConfiguration : IEntityTypeConfiguration<ChargeItem> {
        public void Configure(EntityTypeBuilder<ChargeItem> builder) {
            builder.ToTable("acc_charge_items");
            builder.Property(i => i.Code).IsRequired().HasMaxLength(20);
            builder.Property(i => i.Description).IsRequired().HasMaxLength(200);
            builder.Property(i => i.FixedAmount).HasColumnType("decimal(18,2)");
            builder.Property(i => i.Percentage).HasColumnType("decimal(10,4)");
            builder.Property(i => i.LedgerCode).HasMaxLength(30);
            builder.Property(i => i.CreatedBy).HasMaxLength(100);
            builder.Property(i => i.UpdatedBy).HasMaxLength(100);
            builder.Property(i => i.DeletedBy).HasMaxLength(100);
            builder.HasIndex(i => i.Code).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_charge_items_code");

            builder.HasOne(c => c.Tax)
                .WithMany(c => c.ChargedItems)
                .HasForeignKey(y => y.TaxId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.TimedepositProductChargeItems)
                .WithOne(c => c.ChargeItem)
                .HasForeignKey(y => y.ChargeItemId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.InsuranceProductChargeItems)
                .WithOne(c => c.ChargeItem)
                .HasForeignKey(y => y.ChargeItemId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.ShareProductChargeItems)
                .WithOne(c => c.ChargeItem)
                .HasForeignKey(y => y.ChargeItemId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.SavingProductChargeItems)
                .WithOne(c => c.ChargeItem)
                .HasForeignKey(y => y.ChargeItemId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.LoanProductChargeItems)
                .WithOne(c => c.ChargeItem)
                .HasForeignKey(y => y.ChargeItemId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
