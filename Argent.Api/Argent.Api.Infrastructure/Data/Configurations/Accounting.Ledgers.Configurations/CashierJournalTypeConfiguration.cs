using Argent.Api.Domain.Entities.Accounting.Journals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class CashierJournalTypeConfiguration : IEntityTypeConfiguration<CashierJournalType> {
        public void Configure(EntityTypeBuilder<CashierJournalType> builder) {
            builder.ToTable("acc_cashier_journal_types");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasIndex(cj => new { cj.CashierId, cj.JournalTypeId }).IsUnique()
                .HasFilter("\"IsDeleted\" = false")
                .HasDatabaseName("ux_acc_cashier_journal_types");

            builder.HasOne(cj => cj.Cashier)
                .WithMany(c => c.JournalTypes)
                .HasForeignKey(cj => cj.CashierId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cj => cj.JournalType)
                .WithMany(jt => jt.Cashiers)
                .HasForeignKey(cj => cj.JournalTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
