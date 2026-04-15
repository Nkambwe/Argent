using Argent.Api.Domain.Entities.Kyc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Configurations {
    public class CustomerBaseConfiguration : IEntityTypeConfiguration<CustomerBase> {
        public void Configure(EntityTypeBuilder<CustomerBase> builder) {
            builder.ToTable("customers_base");

            builder.Property(c => c.ClientCode).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Statistic).HasMaxLength(50);
            builder.Property(c => c.Reference).HasMaxLength(50);
            builder.Property(c => c.PermanentAddress).HasMaxLength(300);
            builder.Property(c => c.MailAddress).HasMaxLength(100);
            builder.Property(c => c.PrimaryLine).HasMaxLength(30);
            builder.Property(c => c.SecondaryLine).HasMaxLength(30);
            builder.Property(c => c.Mobile).HasMaxLength(30);
            builder.Property(c => c.Fax).HasMaxLength(30);
            builder.Property(c => c.Email).HasMaxLength(150);
            builder.Property(c => c.City).HasMaxLength(100);
            builder.Property(c => c.Town).HasMaxLength(100);
            builder.Property(c => c.WhatsApp).HasMaxLength(30);
            builder.Property(c => c.Facebook).HasMaxLength(100);
            builder.Property(c => c.Instagram).HasMaxLength(100);
            builder.Property(c => c.Twitter).HasMaxLength(100);
            builder.Property(c => c.Notes).HasMaxLength(1000);
            builder.Property(c => c.ApprovedBy).HasMaxLength(100);
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            // Branch FK
            builder.HasOne(c => c.Branch)
                .WithMany()
                .HasForeignKey(c => c.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // Shared filter FKs — each filter FK points to CustomerFilter with scope guard
            builder.HasOne(c => c.Filter1)
                .WithMany()
                .HasForeignKey(c => c.Filter1Id)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(c => c.Filter2)
                .WithMany()
                .HasForeignKey(c => c.Filter2Id)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(c => c.Filter3)
                .WithMany()
                .HasForeignKey(c => c.Filter3Id)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(c => c.Village)
                .WithMany()
                .HasForeignKey(c => c.VillageId)
                .OnDelete(DeleteBehavior.SetNull);

            // Indexes
            builder.HasIndex(c => c.ClientCode).HasDatabaseName("ix_customers_client_code");
            builder.HasIndex(c => c.BranchId).HasDatabaseName("ix_customers_branch_id");
            builder.HasIndex(c => c.Active).HasDatabaseName("ix_customers_active");
        }
    }
}
