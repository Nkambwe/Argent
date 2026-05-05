using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class BankBranchConfiguration : IEntityTypeConfiguration<BankBranch> {
        public void Configure(EntityTypeBuilder<BankBranch> builder) {
            builder.ToTable("acc_bank_branches");
            builder.Property(b => b.BranchCode).IsRequired().HasMaxLength(20);
            builder.Property(b => b.BranchName).IsRequired().HasMaxLength(150);
            builder.Property(b => b.BranchAddress).HasMaxLength(300);
            builder.Property(b => b.BranchContact).HasMaxLength(200);
            builder.Property(b => b.ContactDesignation).HasMaxLength(100);
            builder.Property(b => b.ContactEmail).HasMaxLength(150);
            builder.Property(b => b.PrimaryLine).HasMaxLength(30);
            builder.Property(b => b.SecondaryLine).HasMaxLength(30);
            builder.Property(b => b.BranchFax).HasMaxLength(30);
            builder.Property(b => b.CreatedBy).HasMaxLength(100);
            builder.Property(b => b.UpdatedBy).HasMaxLength(100);
            builder.Property(b => b.DeletedBy).HasMaxLength(100);
            builder.HasIndex(b => new { b.BankId, b.BranchCode }).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_bank_branches_bank_code");
            builder.HasOne(b => b.Bank).WithMany(bk => bk.Branches)
                .HasForeignKey(b => b.BankId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
