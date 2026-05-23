using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Core.Modules.Products.RequestObjects {
    public class UpdateLoanProductRequest {
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long ProductTypeId { get; set; }
        public long? ChargeGroupId { get; set; }
        public bool VatInclusive { get; set; }
        public bool UseChargeGroups { get; set; }
        public CustomerTarget TargetGroup { get; set; }
        public bool UseClasses { get; set; }
    }
}
