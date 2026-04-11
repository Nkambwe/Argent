using Argent.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations {
    public class BranchConfiguration : IEntityTypeConfiguration<Branch> {
        public void Configure(EntityTypeBuilder<Branch> builder) {
            builder.ToTable("branches");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.BranchCode).IsRequired().HasMaxLength(10);
            builder.Property(b => b.BranchName).IsRequired().HasMaxLength(200);
            builder.Property(b => b.Address).IsRequired().HasMaxLength(300);
            builder.Property(b => b.EmailAddress).IsRequired().HasMaxLength(500);
            builder.Property(b => b.PostalAddress).HasMaxLength(100);
            builder.Property(b => b.IsDefault).HasDefaultValue(false);
            builder.Property(b => b.IsActive).HasDefaultValue(true);

            //..audit columns
            builder.Property(b => b.CreatedBy).HasMaxLength(100);
            builder.Property(b => b.UpdatedBy).HasMaxLength(100);
            builder.Property(b => b.DeletedBy).HasMaxLength(100);

            //..unique constraint for allowing only one default branch per organization
            // Uses a partial unique index so it only applies to active records.
            builder.HasIndex(b => new { b.OrganizationId, b.IsDefault })
                   .HasFilter("\"IsDefault\" = true AND \"IsDeleted\" = false")
                   .IsUnique()
                   .HasDatabaseName("ux_branches_org_default");

            //..add an index for common lookup patterns
            builder.HasIndex(b => b.OrganizationId)
                .HasDatabaseName("ix_branches_organization_id");
        }
    }
}
