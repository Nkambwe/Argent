namespace Argent.Api.Infrastructure.Core.Modules.Access.RequestObjects {
    public class RefreshTokenRequest {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
