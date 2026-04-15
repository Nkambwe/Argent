using Argent.Api.Domain.Entities.Kyc.KycFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Files.Configurations {
    public class CustomerContractConfiguration : IEntityTypeConfiguration<CustomerContract> {
        public void Configure(EntityTypeBuilder<CustomerContract> builder) {
            builder.ToTable("kyc_customer_contracts");
        }
    }

}
