using Argent.Api.Domain.Entities.Support.KycSupport;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Support.Configurations {
    public class IncomeHistoryConfiguration : IEntityTypeConfiguration<IncomeHistory> {
        public void Configure(EntityTypeBuilder<IncomeHistory> builder) {
            builder.ToTable("kyc_income_histories");
            builder.Property(h => h.Employer).HasMaxLength(200);
            builder.Property(h => h.PositionHeld).HasMaxLength(100);
            builder.Property(h => h.Salary).HasColumnType("decimal(18,2)");
            builder.Property(h => h.CreatedBy).HasMaxLength(100);
            builder.Property(h => h.UpdatedBy).HasMaxLength(100);
            builder.Property(h => h.DeletedBy).HasMaxLength(100);
            builder.HasOne(h => h.IncomeType).WithMany().HasForeignKey(h => h.IncomeTypeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(h => new { h.CustomerId, h.CustomerType }).HasDatabaseName("ix_income_histories_customer");
        }
    }
}
