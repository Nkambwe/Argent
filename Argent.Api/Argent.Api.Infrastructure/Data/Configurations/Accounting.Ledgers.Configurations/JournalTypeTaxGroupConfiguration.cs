using Argent.Api.Domain.Entities.Accounting.Journals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class JournalTypeTaxGroupConfiguration : IEntityTypeConfiguration<JournalTypeTaxGroup> {
        public void Configure(EntityTypeBuilder<JournalTypeTaxGroup> builder) {
            builder.ToTable("acc_journal_type_tax_group");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasIndex(cv => new { cv.JournalTypeId, cv.TaxGroupId }).IsUnique()
                .HasFilter("\"IsDeleted\" = false")
                .HasDatabaseName("ux_acc_journal_type_tax_group");

            builder.HasOne(cv => cv.JournalType)
                .WithMany(c => c.TaxGroups)
                .HasForeignKey(cv => cv.JournalTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cv => cv.TaxGroup)
                .WithMany(vt => vt.JournalTypes)
                .HasForeignKey(cv => cv.TaxGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
