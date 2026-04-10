using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Logging;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Common.Behaviour {
    /// <summary>
    /// Runs for every command that implements IBranchPolicyCommand.
    /// Checks (in order):
    ///   1. User is authenticated
    ///   2. User has the required permission
    ///   3. User can access the target branch
    ///   4. User can post to the target branch (not read-only access)
    ///
    /// Holiday and BranchEntityAccess checks will be added here once
    /// those modules are built — the pipeline position is already correct.
    /// </summary>
    public class BranchPolicyBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull {
        private readonly IUserContext _userContext;
        private readonly IServiceLoggerFactory _loggerFactory;

        public BranchPolicyBehavior(IUserContext userContext, IServiceLoggerFactory loggerFactory) {
            _userContext = userContext;
            _loggerFactory = loggerFactory;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken ct) {
            // Only applies to branch-policy commands
            if (request is not IBranchPolicyCommand branchCommand)
                return await next();

            var logger = _loggerFactory.CreateLogger("pipeline");
            logger.Channel = $"BRANCH-POLICY-{typeof(TRequest).Name}";

            // 1. Authentication
            if (!_userContext.IsAuthenticated)
                return Fail("Unauthenticated request.", "UNAUTHENTICATED");

            // 2. Permission check
            if (!_userContext.HasPermission(branchCommand.RequiredPermission)) {
                logger.Log(
                    $"User '{_userContext.Username}' denied — missing permission '{branchCommand.RequiredPermission}'",
                    "ACCESS-DENIED");
                return Fail($"You do not have permission to perform this action ({branchCommand.RequiredPermission}).",
                    "FORBIDDEN");
            }

            // 3. Branch access
            if (!_userContext.CanAccessBranch(branchCommand.TargetBranchId)) {
                logger.Log(
                    $"User '{_userContext.Username}' denied — no access to branch {branchCommand.TargetBranchId}",
                    "ACCESS-DENIED");
                return Fail("You do not have access to the specified branch.", "BRANCH_ACCESS_DENIED");
            }

            // 4. Post permission on branch
            if (!_userContext.CanPostToBranch(branchCommand.TargetBranchId)) {
                logger.Log(
                    $"User '{_userContext.Username}' denied — read-only access to branch {branchCommand.TargetBranchId}",
                    "ACCESS-DENIED");
                return Fail("You have read-only access to this branch.", "BRANCH_READ_ONLY");
            }

            // TODO (when BranchHoliday module is built):
            // 5. Holiday check — if today is a holiday for this branch, block posting

            // TODO (when BranchEntityAccess module is built):
            // 6. Entity access — verify the module/entity is enabled at this branch

            logger.Log(
                $"Branch policy passed for user '{_userContext.Username}' on branch {branchCommand.TargetBranchId}",
                "ACCESS-GRANTED");

            return await next();
        }

        private static TResponse Fail(string error, string code) {
            var responseType = typeof(TResponse);

            if (responseType.IsGenericType &&
                responseType.GetGenericTypeDefinition() == typeof(Result<>)) {
                var innerType = responseType.GetGenericArguments()[0];
                var method = typeof(Result<>)
                    .MakeGenericType(innerType)
                    .GetMethod(nameof(Result<object>.Failure), [typeof(string), typeof(string)])!;
                return (TResponse)method.Invoke(null, [error, code])!;
            }

            if (responseType == typeof(Result))
                return (TResponse)(object)Result.Failure(error, code);

            throw new UnauthorizedAccessException(error);
        }
    }
}
