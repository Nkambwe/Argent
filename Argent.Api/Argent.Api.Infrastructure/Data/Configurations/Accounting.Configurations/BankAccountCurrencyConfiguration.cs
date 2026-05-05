using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class BankAccountCurrencyConfiguration : IEntityTypeConfiguration<BankAccountCurrency> {
        public void Configure(EntityTypeBuilder<BankAccountCurrency> builder) {
            builder.ToTable("acc_bank_account_currencies");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);
            builder.HasIndex(c => new { c.BankAccountId, c.CurrencyId }).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_bank_acct_currencies");
            builder.HasOne(c => c.BankAccount).WithMany(a => a.Currencies)
                .HasForeignKey(c => c.BankAccountId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(c => c.Currency).WithMany()
                .HasForeignKey(c => c.CurrencyId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
