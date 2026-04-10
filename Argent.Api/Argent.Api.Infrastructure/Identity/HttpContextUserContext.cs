using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Data;
using Argent.Api.Infrastructure.Logging;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Argent.Api.Infrastructure.Identity {
    /// <summary>
    /// Resolves the current user context from the HTTP request.
    ///
    /// On first access, reads JWT claims from HttpContext.User, then loads
    /// the user's branch access list from the database. The result is cached
    /// for the lifetime of the request (scoped lifetime).
    ///
    /// The X-Branch-Id header lets a user explicitly set their working branch
    /// for the current request, provided they have access to that branch.
    /// </summary>
    public class HttpContextUserContext(IHttpContextAccessor httpContextAccessor, AppDataContext context, IServiceLoggerFactory loggerFactory) : IUserContext {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly AppDataContext _context = context;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;
        private bool _resolved;
        private long _userId;
        private string _username = string.Empty;
        private string _fullName = string.Empty;
        private long _defaultBranchId;
        private long _currentBranchId;
        private List<long> _accessibleBranchIds = [];
        private List<(long BranchId, bool CanPost)> _branchAccessDetails = [];
        private List<string> _permissions = [];
        private bool _isAuthenticated;
        private string? _ipAddress;

        public long UserId => Resolve()._userId;
        public string Username => Resolve()._username;
        public string FullName => Resolve()._fullName;
        public long DefaultBranchId => Resolve()._defaultBranchId;
        public long CurrentBranchId => Resolve()._currentBranchId;
        public IReadOnlyList<long> AccessibleBranchIds => Resolve()._accessibleBranchIds;
        public IReadOnlyList<string> Permissions => Resolve()._permissions;
        public string? IpAddress => Resolve()._ipAddress;
        public bool IsAuthenticated => Resolve()._isAuthenticated;

        public bool CanAccessBranch(long branchId)
            => Resolve()._accessibleBranchIds.Contains(branchId);

        public bool CanPostToBranch(long branchId)
            => Resolve()._branchAccessDetails.Any(b => b.BranchId == branchId && b.CanPost);

        public bool HasPermission(string permission)
            => Resolve()._permissions.Contains(permission, StringComparer.OrdinalIgnoreCase);

        private HttpContextUserContext Resolve() {
            if (_resolved) return this;

            var logger = _loggerFactory.CreateLogger("user-context");
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext?.User?.Identity?.IsAuthenticated != true) {
                _isAuthenticated = false;
                _resolved = true;
                return this;
            }

            var claims = httpContext.User.Claims.ToList();

            _isAuthenticated = true;
            _userId = long.TryParse(GetClaim(claims, ClaimTypes.NameIdentifier), out var uid) ? uid : 0;
            _username = GetClaim(claims, ClaimTypes.Name) ?? string.Empty;
            _fullName = GetClaim(claims, "fullName") ?? string.Empty;
            _defaultBranchId = long.TryParse(GetClaim(claims, "defualtBranchId"), out var hb) ? hb : 0;
            _permissions = [.. claims.Where(c => c.Type == "permission").Select(c => c.Value)];

            //..IP from connection or forwarded header for proxy setups
            _ipAddress = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? httpContext.Connection.RemoteIpAddress?.ToString();

            // X-Branch-Id header lets the client specify the working branch for this request
            var requestedBranchHeader = httpContext.Request.Headers["X-Branch-Id"].FirstOrDefault();
            var requestedBranchId = long.TryParse(requestedBranchHeader, out var rb) ? rb : 0;

            //..load branch access from DB
            try {
                var branchAccess = _context.UserBranchAccess
                    .Where(ub => ub.UserId == _userId && !ub.IsDeleted)
                    .Select(ub => new { ub.BranchId, ub.CanPost })
                    .ToList();

                _branchAccessDetails = [.. branchAccess.Select(b => (b.BranchId, b.CanPost))];
                _accessibleBranchIds = [.. branchAccess.Select(b => b.BranchId)];

                //..always include home branch
                if (!_accessibleBranchIds.Contains(_defaultBranchId))
                    _accessibleBranchIds.Add(_defaultBranchId);

                //..set current branch — header override if user has access, otherwise home branch
                _currentBranchId = (requestedBranchId != 0 && CanAccessBranch(requestedBranchId))
                    ? requestedBranchId
                    : _defaultBranchId;
            }
            catch (Exception ex) {
                logger.Log($"Failed to load branch access for user {_userId}: {ex.Message}", "ERROR");
                _accessibleBranchIds = [_defaultBranchId];
                _currentBranchId = _defaultBranchId;
            }

            logger.Channel = $"USER-CTX-{_username}";
            logger.Log($"Context resolved: User={_username}, Branch={_currentBranchId}, " +
                $"AccessibleBranches={_accessibleBranchIds.Count}, Permissions={_permissions.Count}", "INFO");

            _resolved = true;
            return this;
        }

        private static string? GetClaim(List<Claim> claims, string type)
            => claims.FirstOrDefault(c => c.Type == type)?.Value;
    }


}
