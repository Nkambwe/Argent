using Argent.Api.Domain.Entities.Accounting.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class TransactionDocumentTypeConfiguration : IEntityTypeConfiguration<TransactionDocumentType> {
        public void Configure(EntityTypeBuilder<TransactionDocumentType> builder) {
            builder.ToTable("acc_document_types");
            builder.Property(t => t.Code).IsRequired().HasMaxLength(20);
            builder.Property(t => t.TypeName).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Notes).HasMaxLength(500);
            builder.Property(t => t.CreatedBy).HasMaxLength(100);
            builder.Property(t => t.UpdatedBy).HasMaxLength(100);
            builder.Property(t => t.DeletedBy).HasMaxLength(100);
            builder.HasIndex(t => t.Code).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_document_types_code");
        }
    }


}
