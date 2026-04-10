using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;

namespace Argent.Api.Infrastructure.Core.Modules.Access {
    public class AuthResponse {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime AccessTokenExpiry { get; set; }
        public UserDto User { get; set; } = new();
    }
}
