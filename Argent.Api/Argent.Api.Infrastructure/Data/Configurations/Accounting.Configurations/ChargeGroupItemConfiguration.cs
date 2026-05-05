using Argent.Api.Domain.Entities.Accounting.Charges;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class ChargeGroupItemConfiguration : IEntityTypeConfiguration<ChargeGroupItem> {
        public void Configure(EntityTypeBuilder<ChargeGroupItem> builder) {
            builder.ToTable("acc_charge_group_items");
            builder.Property(i => i.Code).IsRequired().HasMaxLength(20);
            builder.Property(i => i.ChargeName).IsRequired().HasMaxLength(150);
            builder.Property(i => i.Rate).HasColumnType("decimal(10,4)");
            builder.Property(i => i.FlatAmount).HasColumnType("decimal(18,2)");
            builder.Property(i => i.Notes).HasMaxLength(500);
            builder.Property(i => i.CreatedBy).HasMaxLength(100);
            builder.Property(i => i.UpdatedBy).HasMaxLength(100);
            builder.Property(i => i.DeletedBy).HasMaxLength(100);
            builder.HasOne(i => i.ChargeGroup).WithMany(g => g.Items)
                .HasForeignKey(i => i.ChargeGroupId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
