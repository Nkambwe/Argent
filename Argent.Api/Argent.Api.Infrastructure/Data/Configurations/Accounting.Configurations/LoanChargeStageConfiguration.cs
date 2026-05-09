using Argent.Api.Domain.Entities.Banking.Loans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class LoanChargeStageConfiguration : IEntityTypeConfiguration<LoanChargeStage> {
        public void Configure(EntityTypeBuilder<LoanChargeStage> builder) {
            builder.ToTable("lnr_charge_stages");
            builder.Property(s => s.CreatedBy).HasMaxLength(100);
            builder.Property(s => s.UpdatedBy).HasMaxLength(100);
            builder.Property(s => s.DeletedBy).HasMaxLength(100);
           
            builder.HasIndex(s => new {s.ChargeItemId, s.Id })
                .HasDatabaseName("ix_lnr_charge_stages_charge_item");

            builder.HasOne(s => s.ChargeItem).WithMany(i => i.ChargeStages)
                .HasForeignKey(s => s.ChargeItemId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(s => new { s.LoanProductId, s.Id })
                .HasDatabaseName("ix_lnr_charge_stages_loan_product");

            builder.HasOne(s => s.LoanProduct).WithMany(i => i.ChargeStages)
                .HasForeignKey(s => s.ChargeItemId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
