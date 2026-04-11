using Argent.Api.Domain.Entities.Access;
using System.Security.Claims;

namespace Argent.Api.Infrastructure.Identity {
    public interface IJwtTokenService {
        string GenerateAccessToken(AppUser user, IReadOnlyList<string> permissions, IEnumerable<UserBranchAccess> branchAccess);
        string GenerateRefreshToken();
        ClaimsPrincipal? ValidateExpiredToken(string token);
    }
}
