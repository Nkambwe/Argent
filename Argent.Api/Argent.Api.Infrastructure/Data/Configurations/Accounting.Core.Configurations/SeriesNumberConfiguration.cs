using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Core.Configurations {
    public class SeriesNumberConfiguration : IEntityTypeConfiguration<SeriesNumber> {
        public void Configure(EntityTypeBuilder<SeriesNumber> builder) {
            builder.ToTable("acc_series_numbers");
            builder.Property(s => s.Identifier).IsRequired().HasMaxLength(20);
            builder.Property(s => s.CustomSeries).HasMaxLength(20);
            builder.Property(s => s.Code).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Description).HasMaxLength(200);
            builder.Property(s => s.CreatedBy).HasMaxLength(100);
            builder.Property(s => s.UpdatedBy).HasMaxLength(100);
            builder.Property(s => s.DeletedBy).HasMaxLength(100);

            builder.HasIndex(s => new { s.DocumentTypeId, s.BranchId, s.IsDefault })
                .HasDatabaseName("ix_acc_series_numbers_doctype_branch_default");

            builder.HasOne(s => s.Branch)
                .WithMany()
                .HasForeignKey(s => s.BranchId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(s => s.DocumentType)
                .WithMany(dt => dt.SeriesNumbers)
                .HasForeignKey(s => s.DocumentTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
