namespace Argent.Api.Infrastructure.Core.Modules.Access.DataObjects {
    public class UserDetailDto : UserSummaryDto {
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public long DefaultBranchId { get; set; }
        public string DefaultBranchCode { get; set; } = string.Empty;
        public IEnumerable<BranchAccessDto> BranchAccess { get; set; } = [];
        public DateTime CreatedOn { get; set; }
    }

}
