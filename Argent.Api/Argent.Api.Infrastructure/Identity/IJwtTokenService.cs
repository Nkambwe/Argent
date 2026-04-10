using Argent.Api.Domain.Entities.Access;
using System.Security.Claims;

namespace Argent.Api.Infrastructure.Identity {
    public interface IJwtTokenService {
        string GenerateAccessToken(AppUser user, IEnumerable<string> permissions);
        string GenerateRefreshToken();
        ClaimsPrincipal? ValidateToken(string token);
    }
}
