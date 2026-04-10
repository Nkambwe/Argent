namespace Argent.Api.Infrastructure.Core.Common.Interfaces {
    /// <summary>
    /// Marker interface for commands that must pass branch policy checks before execution:
    ///   1. User has access to the target branch
    ///   2. User can post,not read-only, to the target branch
    ///   3. The target branch is not on a holiday that blocks this operation
    ///   4. The module/entity is accessible at this branch (BranchEntityAccess)
    ///
    /// The BranchPolicyBehavior pipeline reads TargetBranchId and RequiredPermission
    /// to perform these checks automatically.
    /// </summary>
    public interface IBranchPolicyCommand {
        long TargetBranchId { get; }
        string RequiredPermission { get; }  
    }
}
