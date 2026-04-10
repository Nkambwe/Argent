
using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Identity;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class RefreshTokenCommandHandler(
        IUnitOfWork uow,
        IJwtTokenService jwtService,
        IServiceLoggerFactory loggerFactory) : IRequestHandler<RefreshTokenCommand, Result<AuthResponse>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IJwtTokenService _jwtService = jwtService;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<AuthResponse>> Handle(RefreshTokenCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("auth");
            logger.Channel = "REFRESH-TOKEN";

            var existing = await _uow.Access.GetRefreshTokenAsync(command.RefreshToken, ct);

            if (existing is null || existing.IsRevoked || existing.ExpiresAt < DateTime.UtcNow) {
                logger.Log("Refresh token invalid or expired", "AUTH-FAIL");
                return Result<AuthResponse>.Failure("Invalid or expired refresh token.", "INVALID_REFRESH_TOKEN");
            }

            var user = await _uow.Access.GetUserByIdAsync(existing.UserId, ct);
            if (user is null || !user.IsActive)
                return Result<AuthResponse>.Failure("User account is inactive.", "ACCOUNT_INACTIVE");

            var permissions = await _uow.Access.GetPermissionsByUserAsync(user.Id, ct);
            var permissionNames = permissions.Select(p => p.Name).ToList();

            var newAccessToken = _jwtService.GenerateAccessToken(user, permissionNames);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            // Rotate: revoke old, issue new
            existing.IsRevoked = true;
            existing.ReplacedByToken = newRefreshToken;
            _uow.Access.UpdateRefreshToken(existing);

            await _uow.Access.AddRefreshTokenAsync(new RefreshToken
            {
                UserId = user.Id,
                Token = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedByIp = command.IpAddress
            }, ct);

            await _uow.CommitAsync(ct);

            logger.Log($"Refresh token rotated for user {user.Username}", "AUTH");

            return Result<AuthResponse>.Success(new AuthResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                AccessTokenExpiry = DateTime.UtcNow.AddMinutes(60),
                User = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Permissions = permissionNames,
                    IsActive = user.IsActive,
                    LastLoginAt = user.LastLoginAt
                }
            });
        }
    }

}
