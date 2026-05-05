using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Core.Configurations {
    public class LedgerAccountTotalConfiguration : IEntityTypeConfiguration<LedgerAccountTotal> {
        public void Configure(EntityTypeBuilder<LedgerAccountTotal> builder) {
            builder.ToTable("acc_ledger_totals");
            builder.Property(t => t.LedgerNumber).IsRequired().HasMaxLength(30);
            builder.Property(t => t.LedgerName).IsRequired().HasMaxLength(200);
            builder.Property(t => t.TotalRange).IsRequired().HasMaxLength(100);
            builder.Property(t => t.CreatedBy).HasMaxLength(100);
            builder.Property(t => t.UpdatedBy).HasMaxLength(100);
            builder.Property(t => t.DeletedBy).HasMaxLength(100);

            builder.HasOne(t => t.LedgerAccountHeader)
                .WithMany(h => h.TotalLabels)
                .HasForeignKey(t => t.LedgerAccountHeaderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
