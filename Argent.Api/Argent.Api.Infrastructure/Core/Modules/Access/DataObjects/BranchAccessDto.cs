namespace Argent.Api.Infrastructure.Core.Modules.Access.DataObjects {
    public class BranchAccessDto {
        public long BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public bool CanPost { get; set; }
    }
}
