namespace Argent.Api.Infrastructure.Core.Common.Interfaces {
    /// <summary>
    /// The resolved context of the currently authenticated user.
    /// Populated from the JWT token by HttpContextUserContext (Infrastructure layer)
    /// and injected as a scoped service into handlers, services, and the audit pipeline.
    ///
    /// Every command handler that touches branch-scoped data should call
    /// CanAccessBranch() and HasPermission() before proceeding.
    /// </summary>
    public interface IUserContext {
        long UserId { get; }
        string Username { get; }
        string FullName { get; }
        long DefaultBranchId { get; }
        long CurrentBranchId { get; }          
        IReadOnlyList<long> AccessibleBranchIds { get; }
        IReadOnlyList<string> Permissions { get; }
        string? IpAddress { get; }
        bool IsAuthenticated { get; }
        bool CanAccessBranch(long branchId);
        bool HasPermission(string permission);  
        bool CanPostToBranch(long branchId);    
    }
}
