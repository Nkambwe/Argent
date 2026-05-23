namespace Argent.Api.Infrastructure.Core.Modules.Products.RequestObjects {
    public class CreateShareProductRequest {
        public string Code { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long ProductTypeId { get; set; }
        public long? ChargeGroupId { get; set; }
        public bool VatInclusive { get; set; }
        public bool UseChargeGroups { get; set; }
        public List<UpsertPostingAccountRequest> PostingAccounts { get; set; } = [];
    }
}
