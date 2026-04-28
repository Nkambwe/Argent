
using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Identity;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {

    public class RefreshTokenCommandHandler(IUnitOfWork uow, IJwtTokenService tokenService, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<RefreshTokenCommand, Result<AuthResponseDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IJwtTokenService _tokenService = tokenService;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        private const int RefreshExpiryDays = 7;

        public async Task<Result<AuthResponseDto>> Handle(RefreshTokenCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("access");
            logger.Channel = "TOKEN-REFRESH";

            var principal = _tokenService.ValidateExpiredToken(command.AccessToken);
            if (principal is null) {
                return Result<AuthResponseDto>.Failure("Invalid access token.", "INVALID_TOKEN");
            }

            var userIdStr = principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!long.TryParse(userIdStr, out var userId))
                return Result<AuthResponseDto>.Failure("Invalid token claims.", "INVALID_TOKEN");

            //..validate refresh token via IUserRepository
            var storedToken = await _uow.Users.GetRefreshTokenAsync(command.RefreshToken, ct);
            if (storedToken is null || storedToken.UserId != userId)
                return Result<AuthResponseDto>.Failure("Invalid refresh token.", "INVALID_REFRESH_TOKEN");

            if (storedToken.IsRevoked) {
                logger.Log($"Revoked refresh token reused — possible theft. UserId: {userId}", "SECURITY-ALERT");
                return Result<AuthResponseDto>.Failure("Refresh token has been revoked.", "TOKEN_REVOKED");
            }

            if (storedToken.ExpiresOn < DateTime.UtcNow) {
                return Result<AuthResponseDto>.Failure("Refresh token has expired. Please log in again.", "TOKEN_EXPIRED");
            }
            
            var user = await _uow.Users.GetWithAccessAsync(userId, ct);
            if (user is null || !user.IsActive) {
                return Result<AuthResponseDto>.Failure("Account not found or inactive.", "ACCOUNT_INACTIVE");
            }

            var permissions = await _uow.Permissions.GetUserPermissionsAsync(userId, ct);
            var branchAccess = (await _uow.Users.GetBranchAccessAsync(userId, ct)).ToList();

            var newAccessToken = _tokenService.GenerateAccessToken(user, permissions, branchAccess);
            var newRefreshRaw = _tokenService.GenerateRefreshToken();

            storedToken.IsRevoked = true;
            storedToken.ReplacedByToken = newRefreshRaw;

            var newToken = new RefreshToken {
                UserId = userId,
                Token = newRefreshRaw,
                ExpiresOn = DateTime.UtcNow.AddDays(RefreshExpiryDays),
                CreatedByIp = command.IpAddress
            };

            return await _uow.ExecuteInTransactionAsync(async token =>
            {
                _uow.Users.UpdateRefreshToken(storedToken);
                await _uow.Users.AddRefreshTokenAsync(newToken, token);

                logger.Log($"Token refreshed: {user.Username}", "AUTH-OK");

                var homeAccess = new BranchAccessDto {
                    BranchId = user.DefaultBranchId,
                    BranchName = user.DefaultBranch?.BranchName ?? string.Empty,
                    CanPost = true
                };

                return Result<AuthResponseDto>.Success(new AuthResponseDto
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshRaw,
                    ExpiresOn = DateTime.UtcNow.AddMinutes(60),
                    UserId = user.Id,
                    Username = user.Username,
                    FullName = string.IsNullOrEmpty(user.MiddleName)?
                    $"{user.FirstName} {user.LastName}".Trim():
                    $"{user.FirstName} {user.MiddleName} {user.LastName}".Trim(),
                    DefaultBranchId = user.DefaultBranchId,
                    Permissions = permissions,
                    AccessibleBranches = [.. branchAccess
                        .Where(ba => ba.BranchId != user.DefaultBranchId)
                        .Select(ba => new BranchAccessDto
                        {
                            BranchId = ba.BranchId,
                            BranchName = ba.Branch?.BranchName ?? string.Empty,
                            CanPost = ba.CanPost
                        })
                        .Prepend(homeAccess)]
                });
            }, ct);
        }
    }

}
