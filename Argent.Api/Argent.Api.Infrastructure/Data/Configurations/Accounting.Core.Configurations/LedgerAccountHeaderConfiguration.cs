using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Core.Configurations {
    public class LedgerAccountHeaderConfiguration : IEntityTypeConfiguration<LedgerAccountHeader> {
        public void Configure(EntityTypeBuilder<LedgerAccountHeader> builder) {
            builder.ToTable("acc_ledger_headers");
            builder.Property(h => h.LedgerNumber).IsRequired().HasMaxLength(30);
            builder.Property(h => h.LedgerName).IsRequired().HasMaxLength(200);
            builder.Property(h => h.ParentHeader).HasMaxLength(30);
            builder.Property(h => h.CreatedBy).HasMaxLength(100);
            builder.Property(h => h.UpdatedBy).HasMaxLength(100);
            builder.Property(h => h.DeletedBy).HasMaxLength(100);
            builder.HasIndex(h => h.LedgerNumber).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_ledger_headers_number");
        }
    }
}
