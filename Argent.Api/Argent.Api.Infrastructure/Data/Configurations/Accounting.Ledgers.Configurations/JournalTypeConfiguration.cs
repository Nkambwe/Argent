using Argent.Api.Domain.Entities.Accounting.Journals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class JournalTypeConfiguration : IEntityTypeConfiguration<JournalType> {
        public void Configure(EntityTypeBuilder<JournalType> builder) {
            builder.ToTable("acc_journal_types");
            builder.Property(j => j.SeriesIdentifier).IsRequired().HasMaxLength(20);
            builder.Property(j => j.JournalName).IsRequired().HasMaxLength(150);
            builder.Property(j => j.DefaultLedgerNumber).HasMaxLength(30);
            builder.Property(j => j.ReferenceValue1).HasMaxLength(50);
            builder.Property(j => j.ReferenceValue2).HasMaxLength(50);
            builder.Property(j => j.ReferenceValue3).HasMaxLength(50);
            builder.Property(j => j.ReferenceValue4).HasMaxLength(50);
            builder.Property(j => j.ReferenceValue5).HasMaxLength(50);
            builder.Property(j => j.ReferenceValue6).HasMaxLength(50);
            builder.Property(j => j.CreatedBy).HasMaxLength(100);
            builder.Property(j => j.UpdatedBy).HasMaxLength(100);
            builder.Property(j => j.DeletedBy).HasMaxLength(100);

            builder.HasIndex(j => j.JournalName).IsUnique()
                .HasFilter("\"IsDeleted\" = false")
                .HasDatabaseName("ux_acc_journal_types_name");

            builder.HasOne(j => j.GeneralPostingGroup)
                .WithMany()
                .HasForeignKey(j => j.GeneralPostingGroupId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(j => j.BranchPostingGroup)
                .WithMany()
                .HasForeignKey(j => j.BranchPostingGroupId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(j => j.BusinessPostingGroup)
                .WithMany()
                .HasForeignKey(j => j.BusinessPostingGroupId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

}
