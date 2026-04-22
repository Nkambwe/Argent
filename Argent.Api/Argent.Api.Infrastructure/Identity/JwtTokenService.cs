using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Argent.Api.Infrastructure.Identity {
    public class JwtTokenService(IOptions<JwtSettings> jwtOptions) : IJwtTokenService {
        private readonly JwtSettings _jwt = jwtOptions.Value;

        public string GenerateAccessToken(
            AppUser user,
            IReadOnlyList<string> permissions,
            IEnumerable<UserBranchAccess> branchAccess) {
            var branchAccessList = branchAccess.ToList();

            var accessibleBranches = branchAccessList
                .Select(ba => ba.BranchId.ToString())
                .Append(user.DefaultBranchId.ToString())
                .Distinct();

            var postableBranches = branchAccessList
                .Where(ba => ba.CanPost)
                .Select(ba => ba.BranchId.ToString())
                .Append(user.DefaultBranchId.ToString())
                .Distinct();

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new("username",user.Username),
                new("fullname", !string.IsNullOrWhiteSpace(user.MiddleName)? $"{user.FirstName} {user.MiddleName} {user.LastName}".Trim(): $"{user.FirstName} {user.LastName}".Trim()),
                new("defualtbranch",  user.DefaultBranchId.ToString()),
                new("permissions", string.Join(',', permissions)),
                new("branches",    string.Join(',', accessibleBranches)),
                new("postbranches",string.Join(',', postableBranches)),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), new(JwtRegisteredClaimNames.Iat,
                    DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_jwt.AccessTokenExpiryMinutes);
            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken() {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(bytes);
        }

        public ClaimsPrincipal? ValidateExpiredToken(string token) {
            var parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey)),
                ValidateIssuer = true,
                ValidIssuer = _jwt.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwt.Audience,
                ValidateLifetime = false  
            };

            try {
                var principal = new JwtSecurityTokenHandler()
                    .ValidateToken(token, parameters, out var validatedToken);

                if (validatedToken is not JwtSecurityToken jwtToken ||
                    !jwtToken.Header.Alg.Equals(
                        SecurityAlgorithms.HmacSha256,
                        StringComparison.OrdinalIgnoreCase))
                    return null;

                return principal;
            }
            catch {
                return null;
            }
        }

    }
}
