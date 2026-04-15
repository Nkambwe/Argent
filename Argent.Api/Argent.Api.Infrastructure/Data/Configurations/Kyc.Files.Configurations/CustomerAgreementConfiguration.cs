using Argent.Api.Domain.Entities.Kyc.KycFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Files.Configurations {
    public class CustomerAgreementConfiguration : IEntityTypeConfiguration<CustomerAgreement> {
        public void Configure(EntityTypeBuilder<CustomerAgreement> builder) {
            builder.ToTable("kyc_customer_agreements");
            builder.Property(a => a.Document).HasMaxLength(300);
        }
    }

}
