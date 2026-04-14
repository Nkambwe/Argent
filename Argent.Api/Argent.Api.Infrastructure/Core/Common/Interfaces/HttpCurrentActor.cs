using Microsoft.AspNetCore.Http;

namespace Argent.Api.Infrastructure.Core.Common.Interfaces {
    public class HttpCurrentActor(IHttpContextAccessor accessor) : ICurrentActor {
        public bool IsAuthenticated =>
            accessor.HttpContext?.User?.Identity?.IsAuthenticated == true;

        public string Username =>
            IsAuthenticated
                ? accessor.HttpContext?.User?.Identity?.Name ?? "system"
                : "system";
    }
}
