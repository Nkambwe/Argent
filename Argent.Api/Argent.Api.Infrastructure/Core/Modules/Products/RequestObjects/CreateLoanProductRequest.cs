using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Core.Modules.Products.RequestObjects {
    public class CreateLoanProductRequest {
        public string Code { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long ProductTypeId { get; set; }
        public long? ChargeGroupId { get; set; }
        public bool VatInclusive { get; set; }
        public bool UseChargeGroups { get; set; }
        public CustomerTarget TargetGroup { get; set; } = CustomerTarget.All;
        public bool UseClasses { get; set; }
        public List<UpsertPostingAccountRequest> PostingAccounts { get; set; } = [];
    }
}
