using Argent.Api.Domain.Entities.Access;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Argent.Api.Infrastructure.Identity {
    public class JwtTokenService(IConfiguration configuration) : IJwtTokenService {
        private readonly IConfiguration _configuration = configuration;

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
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat,
                    DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetRequiredSetting("Jwt:Secret")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(
                _configuration.GetValue<int>("Jwt:AccessTokenExpiryMinutes", 60));

            var token = new JwtSecurityToken(
                issuer: GetRequiredSetting("Jwt:Issuer"),
                audience: GetRequiredSetting("Jwt:Audience"),
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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetRequiredSetting("Jwt:Secret"))),
                ValidateIssuer = true,
                ValidIssuer = GetRequiredSetting("Jwt:Issuer"),
                ValidateAudience = true,
                ValidAudience = GetRequiredSetting("Jwt:Audience"),
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

        private string GetRequiredSetting(string key) =>
            _configuration[key] ?? throw new InvalidOperationException(
                $"Missing required JWT configuration: {key}");
    }
}
