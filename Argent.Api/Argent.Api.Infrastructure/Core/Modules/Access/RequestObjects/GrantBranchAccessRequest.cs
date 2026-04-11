namespace Argent.Api.Infrastructure.Core.Modules.Access.RequestObjects {
    public class GrantBranchAccessRequest { 
        public long BranchId { get; set; }
        public bool CanPost { get; set; }

    }
}
