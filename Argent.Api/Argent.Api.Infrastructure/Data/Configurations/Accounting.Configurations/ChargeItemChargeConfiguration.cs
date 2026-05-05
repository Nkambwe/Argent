using Argent.Api.Domain.Entities.Accounting.Charges;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class ChargeItemChargeConfiguration : IEntityTypeConfiguration<ChargeItemCharge> {
        public void Configure(EntityTypeBuilder<ChargeItemCharge> builder) {
            builder.ToTable("acc_charge_item_charges");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);
            builder.HasIndex(c => new { c.ChargeItemId, c.ChargeId }).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_charge_item_charges");
            builder.HasOne(c => c.ChargeItem).WithMany(i => i.Charges)
                .HasForeignKey(c => c.ChargeItemId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(c => c.Charge).WithMany(ch => ch.ChargeItems)
                .HasForeignKey(c => c.ChargeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
