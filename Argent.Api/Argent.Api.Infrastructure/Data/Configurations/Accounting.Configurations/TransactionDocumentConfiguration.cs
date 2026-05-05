using Argent.Api.Domain.Entities.Accounting.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class TransactionDocumentConfiguration : IEntityTypeConfiguration<TransactionDocument> {
        public void Configure(EntityTypeBuilder<TransactionDocument> builder) {
            builder.ToTable("acc_transaction_documents");
            builder.Property(d => d.TransactionCode).IsRequired().HasMaxLength(50);
            builder.Property(d => d.DocumentNumber).IsRequired().HasMaxLength(50);
            builder.Property(d => d.DocumentName).IsRequired().HasMaxLength(200);
            builder.Property(d => d.Notes).HasMaxLength(500);
            builder.Property(d => d.CreatedBy).HasMaxLength(100);
            builder.Property(d => d.UpdatedBy).HasMaxLength(100);
            builder.Property(d => d.DeletedBy).HasMaxLength(100);
            builder.HasIndex(d => d.TransactionCode).HasDatabaseName("ix_acc_transaction_docs_code");
            builder.HasIndex(d => d.DocumentNumber).HasDatabaseName("ix_acc_transaction_docs_number");
            builder.HasOne(d => d.DocumentType).WithMany(t => t.Documents)
                .HasForeignKey(d => d.DocumentTypeId).OnDelete(DeleteBehavior.Restrict);
        }
    }


}
