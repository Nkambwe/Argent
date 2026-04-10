namespace Argent.Api.Infrastructure.Core.Modules.Access.RequestObjects {
    public class AssignBranchAccessRequest {
        public Guid BranchId { get; set; }
        public bool CanPost { get; set; } = true;
    }
}
