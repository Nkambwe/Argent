using Argent.Api.Domain.Entities.Support.KycLookup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Support.Configurations {
    public class CustomerFilterConfiguration : IEntityTypeConfiguration<CustomerFilter> {
        public void Configure(EntityTypeBuilder<CustomerFilter> builder) {
            builder.ToTable("kyc_customer_filters");
            builder.Property(f => f.Code).IsRequired().HasMaxLength(20);
            builder.Property(f => f.Description).IsRequired().HasMaxLength(200);
            builder.Property(f => f.Notes).HasMaxLength(500);
            builder.Property(f => f.CreatedBy).HasMaxLength(100);
            builder.Property(f => f.UpdatedBy).HasMaxLength(100);
            builder.Property(f => f.DeletedBy).HasMaxLength(100);
            // Scope + Slot + Code must be unique
            builder.HasIndex(f => new { f.Scope, f.SlotNumber, f.Code }).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_customer_filters_scope_slot_code");
        }
    }
}
