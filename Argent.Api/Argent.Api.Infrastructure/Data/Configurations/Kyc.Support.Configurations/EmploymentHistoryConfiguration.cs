using Argent.Api.Domain.Entities.Kyc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Support.Configurations {
    public class EmploymentHistoryConfiguration : IEntityTypeConfiguration<EmploymentHistory> {
        public void Configure(EntityTypeBuilder<EmploymentHistory> builder) {
            builder.ToTable("kyc_employment_histories");
            builder.Property(h => h.Employer).IsRequired().HasMaxLength(200);
            builder.Property(h => h.PositionHeld).HasMaxLength(100);
            builder.Property(h => h.Earning).HasColumnType("decimal(18,2)");
            builder.Property(h => h.Notes).HasMaxLength(500);
            builder.Property(h => h.CreatedBy).HasMaxLength(100);
            builder.Property(h => h.UpdatedBy).HasMaxLength(100);
            builder.Property(h => h.DeletedBy).HasMaxLength(100);
            builder.HasIndex(h => new { h.CustomerId, h.CustomerType }).HasDatabaseName("ix_employment_histories_customer");
        }
    }
}
