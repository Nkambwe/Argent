namespace Argent.Api.Infrastructure.Core.Modules.Access.DataObjects {
    public class UserDetailDto : UserSummaryDto {
        public string? MiddleName { get; set; }
        public long DefaultBranchId { get; set; }
        public IEnumerable<BranchAccessDto> BranchAccess { get; set; } = [];
        public DateTime CreatedOn { get; set; }
    }

}
