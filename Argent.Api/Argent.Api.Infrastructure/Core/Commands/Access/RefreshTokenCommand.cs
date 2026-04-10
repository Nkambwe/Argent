using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public record RefreshTokenCommand(string RefreshToken, string? IpAddress)
        : IRequest<Result<AuthResponse>>;
}
