namespace Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects {
    public class InsuranceProductDetailDto : InsuranceProductSummaryDto {
        public bool VatInclusive { get; set; }
        public bool UseChargeGroups { get; set; }
        public long ProductTypeId { get; set; }
        public long? ChargeGroupId { get; set; }
        public string? Description { get; set; }
        public InsuranceProductConfigDto? Configuration { get; set; }
        public IEnumerable<PostingAccountDto> PostingAccounts { get; set; } = [];
        public IEnumerable<ProductParamDto> Params { get; set; } = [];
    }

}
