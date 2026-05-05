using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class ChequeConfiguration : IEntityTypeConfiguration<Cheque> {
        public void Configure(EntityTypeBuilder<Cheque> builder) {
            builder.ToTable("acc_cheques");
            builder.Property(c => c.Number).IsRequired().HasMaxLength(30);
            builder.Property(c => c.IssuerAccount).HasMaxLength(50);
            builder.Property(c => c.Recipient).HasMaxLength(200);
            builder.Property(c => c.RecipientAccount).HasMaxLength(50);
            builder.Property(c => c.Amount).HasColumnType("decimal(18,2)");
            builder.Property(c => c.AmountInWords).HasMaxLength(300);
            builder.Property(c => c.Notes).HasMaxLength(500);
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);
            builder.HasIndex(c => new { c.ChequeBookId, c.Number }).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_cheques_book_number");
            builder.HasOne(c => c.ChequeBook).WithMany(b => b.Cheques)
                .HasForeignKey(c => c.ChequeBookId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
