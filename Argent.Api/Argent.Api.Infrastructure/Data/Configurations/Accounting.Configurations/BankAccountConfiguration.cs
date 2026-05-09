using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount> {
        public void Configure(EntityTypeBuilder<BankAccount> builder) {
            builder.ToTable("acc_bank_accounts");
            builder.Property(a => a.HolderCode).HasMaxLength(50);
            builder.Property(a => a.AccountName).IsRequired().HasMaxLength(512);
            builder.Property(a => a.AccountNumber).IsRequired().HasMaxLength(512);
            builder.Property(a => a.IbanNumber).HasMaxLength(50);
            builder.Property(a => a.SwiftNumber).HasMaxLength(20);
            builder.Property(a => a.CreditLimit).HasColumnType("decimal(18,2)");
            builder.Property(a => a.CreatedBy).HasMaxLength(100);
            builder.Property(a => a.UpdatedBy).HasMaxLength(100);
            builder.Property(a => a.DeletedBy).HasMaxLength(100);

            builder.HasIndex(a => new { a.BankBranchId, a.AccountNumber }).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_bank_accounts_branch_number");

            builder.HasOne(a => a.BankBranch).WithMany(bb => bb.Accounts)
                .HasForeignKey(a => a.BankBranchId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.LedgerAccount).WithMany()
                .HasForeignKey(a => a.LedgerAccountId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
