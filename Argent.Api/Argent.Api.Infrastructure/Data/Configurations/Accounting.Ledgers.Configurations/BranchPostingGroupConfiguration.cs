using Argent.Api.Domain.Entities.Accounting.Postings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class BranchPostingGroupConfiguration : IEntityTypeConfiguration<BranchPostingGroup> {
        public void Configure(EntityTypeBuilder<BranchPostingGroup> builder) {
            builder.ToTable("acc_branch_posting_groups");
            builder.Property(g => g.Code).IsRequired().HasMaxLength(20);
            builder.Property(g => g.Description).IsRequired().HasMaxLength(200);

            builder.Property(g => g.ReceivablesAccountNumber).IsRequired().HasMaxLength(10);
            builder.Property(g => g.PayablesAccountNumber).IsRequired().HasMaxLength(10);

            builder.Property(g => g.Notes).HasMaxLength(500);
            builder.Property(g => g.CreatedBy).HasMaxLength(100);
            builder.Property(g => g.UpdatedBy).HasMaxLength(100);
            builder.Property(g => g.DeletedBy).HasMaxLength(100);
            builder.HasIndex(g => g.Code).IsUnique()
                .HasFilter("\"IsDeleted\" = false")
                .HasDatabaseName("ux_acc_branch_posting_groups_code");
        }
    }

}
