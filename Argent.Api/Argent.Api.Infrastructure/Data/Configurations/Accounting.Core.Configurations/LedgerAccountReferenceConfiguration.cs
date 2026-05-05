using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Core.Configurations {
    public class LedgerAccountReferenceConfiguration : IEntityTypeConfiguration<LedgerAccountReference> {
        public void Configure(EntityTypeBuilder<LedgerAccountReference> builder) {
            builder.ToTable("acc_ledger_account_references");
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);
            builder.HasIndex(r => new { r.LedgerAccountId, r.ReferenceId }).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_ledger_refs_account_ref");
            builder.HasOne(r => r.LedgerAccount)
                .WithMany(a => a.References)
                .HasForeignKey(r => r.LedgerAccountId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(r => r.Reference)
                .WithMany(ar => ar.LedgerAccounts)
                .HasForeignKey(r => r.ReferenceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
