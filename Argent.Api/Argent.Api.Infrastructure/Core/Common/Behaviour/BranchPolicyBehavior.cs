using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Data;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
    public class BranchPolicyBehavior<TRequest, TResponse>(IUserContext userContext, ISystemConfigurationService configService, AppDataContext dbContext, IServiceLoggerFactory loggerFactory)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull {
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;
        private readonly ISystemConfigurationService _configService = configService;

        private readonly AppDataContext _dbContext = dbContext;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct) {
            //..only applies to branch-policy commands
            if (request is not IBranchPolicyCommand branchCommand)
                return await next();

            var logger = _loggerFactory.CreateLogger("pipeline");
            logger.Channel = $"BRANCH-POLICY-{typeof(TRequest).Name}";

            //..check if user is authentication
            if (!_userContext.IsAuthenticated)
                return Fail("Unauthenticated request.", "UNAUTHENTICATED");

            // ..check if user has a certain permission
            if (!_userContext.HasPermission(branchCommand.RequiredPermission)) {
                logger.Log($"User '{_userContext.Username}' denied — missing permission '{branchCommand.RequiredPermission}'", "ACCESS-DENIED");
                return Fail($"You do not have permission to perform this action ({branchCommand.RequiredPermission}).", "FORBIDDEN");
            }

            //..check if user can access branch
            if (!_userContext.CanAccessBranch(branchCommand.TargetBranchId)) {
                logger.Log($"User '{_userContext.Username}' denied — no access to branch {branchCommand.TargetBranchId}", "ACCESS-DENIED");
                return Fail("You do not have access to the specified branch.", "BRANCH_ACCESS_DENIED");
            }

            //..post permission on branch
            if (!_userContext.CanPostToBranch(branchCommand.TargetBranchId)) {
                logger.Log(
                    $"User '{_userContext.Username}' denied — read-only access to branch {branchCommand.TargetBranchId}",
                    "ACCESS-DENIED");
                return Fail("You have read-only access to this branch.", "BRANCH_READ_ONLY");
            }

            //..check for holiday or non-working day check
            var holidayCheck = await CheckWorkingDayAsync(branchCommand.TargetBranchId, _userContext, logger, ct);
            if (!holidayCheck.IsAllowed)
                return Fail(holidayCheck.Message, holidayCheck.ErrorCode);


            // TODO (when BranchEntityAccess module is built):
            // 6. Entity access — verify the module/entity is enabled at this branch

            logger.Log($"Branch policy passed for user '{_userContext.Username}' on branch {branchCommand.TargetBranchId}", "ACCESS-GRANTED");
            return await next();
        }

        private async Task<(bool IsAllowed, string Message, string ErrorCode)> CheckWorkingDayAsync(long branchId, IUserContext userContext, IServiceLogger logger, CancellationToken ct) {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var dayOfWeek = today.DayOfWeek;

            //..resolve the user's role group, get the first group found — typically users belong to one
            var roleGroupId = await ResolveUserRoleGroupAsync(userContext.UserId, ct);

            //..Weekend check
            if (dayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday) {
                var allowWeekend = await _configService.GetPolicyBoolAsync("AllowWeekendWork", roleGroupId, defaultValue: false, ct);

                if (!allowWeekend) {
                    logger.Log($"Weekend work blocked for user '{userContext.Username}' " +
                        $"(RoleGroup: {roleGroupId})", "POLICY-BLOCK");
                    return (false,"Transactions are not allowed on weekends. Contact your administrator if this is an error.", "WEEKEND_RESTRICTED");
                }
            }

            //..check branch holidays, check for one-time holidays on today's exact date
            var hasHoliday = await _dbContext.BranchHolidays.Where(h => h.BranchId == branchId && !h.IsDeleted && h.IsActive)
                .AnyAsync(h =>
                    //..one-time: exact date match
                    (h.Recurrence == HolidayRecurrence.OneTime && h.HolidayDate == today)
                    ||
                    //..annual: same month and day every year
                    (h.Recurrence == HolidayRecurrence.Annual && h.HolidayDate.Month == today.Month && h.HolidayDate.Day == today.Day),
                    ct);

            if (hasHoliday) {
                var allowHolidayWork = await _configService.GetPolicyBoolAsync("AllowHolidayWork", roleGroupId, defaultValue: false, ct);

                if (!allowHolidayWork) {
                    logger.Log($"Holiday work blocked for user '{userContext.Username}' " +
                               $"on branch {branchId} for date {today}", "POLICY-BLOCK");
                    return (false,
                        $"Today ({today:dd MMM yyyy}) is a non-working day for this branch.",
                        "HOLIDAY_RESTRICTED");
                }
            }

            return (true, string.Empty, string.Empty);
        }

        private async Task<long?> ResolveUserRoleGroupAsync(long userId, CancellationToken ct) {
            // Get the first role group that any of the user's roles belong to
            return await _dbContext.RoleGroupMembers
                .Where(m => !m.IsDeleted  && m.Role.UserRoles.Any(ur => ur.UserId == userId && !ur.IsDeleted))
                .Select(m => (long?)m.RoleGroupId)
                .FirstOrDefaultAsync(ct);
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
