using Argent.Api.Domain.Entities.Kyc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Support.Configurations {
    public class CustomerContactConfiguration : IEntityTypeConfiguration<CustomerContact> {
        public void Configure(EntityTypeBuilder<CustomerContact> builder) {
            builder.ToTable("kyc_customer_contacts");
            builder.Property(c => c.Series).HasMaxLength(20);
            builder.Property(c => c.ContactName).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Telephone).HasMaxLength(30);
            builder.Property(c => c.Mobile).HasMaxLength(30);
            builder.Property(c => c.Email).HasMaxLength(150);
            builder.Property(c => c.Relationship).HasMaxLength(100);
            builder.Property(c => c.Notes).HasMaxLength(500);
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);
            builder.HasIndex(c => new { c.CustomerId, c.CustomerType }).HasDatabaseName("ix_customer_contacts_customer");
        }
    }
}
