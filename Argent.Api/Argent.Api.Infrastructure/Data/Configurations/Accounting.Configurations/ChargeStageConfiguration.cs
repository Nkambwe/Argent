using Argent.Api.Domain.Entities.Accounting.Charges;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class ChargeStageConfiguration : IEntityTypeConfiguration<ChargeStage> {
        public void Configure(EntityTypeBuilder<ChargeStage> builder) {
            builder.ToTable("acc_charge_stages");
            builder.Property(s => s.CreatedBy).HasMaxLength(100);
            builder.Property(s => s.UpdatedBy).HasMaxLength(100);
            builder.Property(s => s.DeletedBy).HasMaxLength(100);
            builder.HasIndex(s => new { s.ChargeItemId, s.ProductId, s.ProductType })
                .HasDatabaseName("ix_acc_charge_stages_item_product");
            builder.HasOne(s => s.ChargeItem).WithMany(i => i.ChargeStages)
                .HasForeignKey(s => s.ChargeItemId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
