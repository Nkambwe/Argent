using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public record RefreshTokenCommand(
        string AccessToken,
        string RefreshToken,
        string? IpAddress
    ) : IRequest<Result<AuthResponseDto>>;
}
