using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Kyc.KycFiles;
using Argent.Api.Domain.Enums;


namespace Argent.Api.Domain.Entities.Banking.Savings {
    /// <summary>
    /// Saving account partner (co-applicant on a savings product).
    /// </summary>
    public class SavingPartner : BaseEntity {
        public long CustomerId { get; set; }
        public CustomerType CustomerType { get; set; }
        public string? Notes { get; set; }
        public ICollection<Identification> Identifications { get; set; } = [];
        public ICollection<ImageFile> Images { get; set; } = [];
    }
}
