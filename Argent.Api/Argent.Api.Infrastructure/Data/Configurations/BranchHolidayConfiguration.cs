using Argent.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations {
    public class BranchHolidayConfiguration : IEntityTypeConfiguration<BranchHoliday> {
        public void Configure(EntityTypeBuilder<BranchHoliday> builder) {
            builder.ToTable("branch_holidays");
            builder.Property(h => h.Name).IsRequired().HasMaxLength(150);
            builder.Property(h => h.Notes).HasMaxLength(300);
            builder.Property(h => h.CreatedBy).HasMaxLength(100);
            builder.Property(h => h.UpdatedBy).HasMaxLength(100);
            builder.Property(h => h.DeletedBy).HasMaxLength(100);

            builder.HasIndex(h => new { h.BranchId, h.HolidayDate }).IsUnique().HasDatabaseName("ux_branch_holidays_branch_date");
            builder.HasIndex(h => h.HolidayDate).HasDatabaseName("ix_branch_holidays_date");
        }
    }
}
