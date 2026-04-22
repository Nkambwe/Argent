using Argent.Api.Infrastructure.Core.Common;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public record LogoutCommand(string RefreshToken, string Ip, long UserId)
    : IRequest<Result<bool>>;
}
