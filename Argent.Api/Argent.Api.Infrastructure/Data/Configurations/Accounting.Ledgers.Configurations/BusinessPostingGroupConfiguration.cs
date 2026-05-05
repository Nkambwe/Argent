using Argent.Api.Domain.Entities.Accounting.Postings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class BusinessPostingGroupConfiguration : IEntityTypeConfiguration<BusinessPostingGroup> {
        public void Configure(EntityTypeBuilder<BusinessPostingGroup> builder) {
            builder.ToTable("acc_business_posting_groups");
            builder.Property(g => g.Code).IsRequired().HasMaxLength(20);
            builder.Property(g => g.Description).IsRequired().HasMaxLength(200);
            builder.Property(g => g.Notes).HasMaxLength(500);
            builder.Property(g => g.CreatedBy).HasMaxLength(100);
            builder.Property(g => g.UpdatedBy).HasMaxLength(100);
            builder.Property(g => g.DeletedBy).HasMaxLength(100);
            builder.HasIndex(g => g.Code).IsUnique()
                .HasFilter("\"IsDeleted\" = false")
                .HasDatabaseName("ux_acc_business_posting_groups_code");
        }
    }

}
