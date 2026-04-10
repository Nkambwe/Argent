using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public record LoginCommand(string Username,string Password,string? IpAddress) 
        : IRequest<Result<AuthResponse>>;
}
