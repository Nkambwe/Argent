using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class ChequeBookConfiguration : IEntityTypeConfiguration<ChequeBook> {
        public void Configure(EntityTypeBuilder<ChequeBook> builder) {
            builder.ToTable("acc_cheque_books");
            builder.Property(b => b.SerialNumber).IsRequired().HasMaxLength(50);
            builder.Property(b => b.FirstChequeNumber).IsRequired().HasMaxLength(30);
            builder.Property(b => b.LastChequeNumber).IsRequired().HasMaxLength(30);
            builder.Property(b => b.LastIssuedCheque).HasMaxLength(30);
            builder.Property(b => b.CreatedBy).HasMaxLength(100);
            builder.Property(b => b.UpdatedBy).HasMaxLength(100);
            builder.Property(b => b.DeletedBy).HasMaxLength(100);
            builder.HasIndex(b => b.SerialNumber).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_cheque_books_serial");
            builder.HasOne(b => b.BankAccount).WithMany(a => a.ChequeBooks)
                .HasForeignKey(b => b.BankAccountId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
