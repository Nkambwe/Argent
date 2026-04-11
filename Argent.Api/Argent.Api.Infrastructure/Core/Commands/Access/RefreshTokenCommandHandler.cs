
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
        IJwtTokenService tokenService,
        IServiceLoggerFactory loggerFactory) : IRequestHandler<RefreshTokenCommand, Result<AuthResponseDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IJwtTokenService _tokenService = tokenService;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        private const int RefreshExpiryDays = 7;

        public async Task<Result<AuthResponseDto>> Handle(RefreshTokenCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("access");
            logger.Channel = "TOKEN-REFRESH";

            // Validate the expired access token structure (lifetime NOT checked)
            var principal = _tokenService.ValidateExpiredToken(command.AccessToken);
            if (principal is null)
                return Result<AuthResponseDto>.Failure("Invalid access token.", "INVALID_TOKEN");

            var userIdStr = principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!long.TryParse(userIdStr, out var userId))
                return Result<AuthResponseDto>.Failure("Invalid token claims.", "INVALID_TOKEN");

            // Validate refresh token
            var storedToken = await _uow.Access.GetRefreshTokenAsync(command.RefreshToken, ct);
            if (storedToken is null || storedToken.UserId != userId)
                return Result<AuthResponseDto>.Failure("Invalid refresh token.", "INVALID_REFRESH_TOKEN");

            if (storedToken.IsRevoked) {
                logger.Log($"Revoked refresh token reused — possible token theft. UserId: {userId}", "SECURITY-ALERT");
                return Result<AuthResponseDto>.Failure("Refresh token has been revoked.", "TOKEN_REVOKED");
            }

            if (storedToken.ExpiresAt < DateTime.UtcNow)
                return Result<AuthResponseDto>.Failure(
                    "Refresh token has expired. Please log in again.", "TOKEN_EXPIRED");

            //..load user with full access
            var user = await _uow.Access.GetByIdWithAccessAsync(userId, ct);
            if (user is null || !user.IsActive)
                return Result<AuthResponseDto>.Failure("Account not found or inactive.", "ACCOUNT_INACTIVE");

            var permissions = await _uow.Access.GetUserPermissionsAsync(userId, ct);
            var branchAccess = (await _uow.Access.GetUserBranchAccessAsync(userId, ct)).ToList();

            //..rotate tokens
            var newAccessToken = _tokenService.GenerateAccessToken(user, permissions, branchAccess);
            var newRefreshRaw = _tokenService.GenerateRefreshToken();

            storedToken.IsRevoked = true;
            storedToken.ReplacedByToken = newRefreshRaw;

            var newRefreshToken = new RefreshToken
            {
                UserId = userId,
                Token = newRefreshRaw,
                ExpiresAt = DateTime.UtcNow.AddDays(RefreshExpiryDays),
                CreatedByIp = command.IpAddress
            };

            await _uow.BeginTransactionAsync(ct);
            try {
                _uow.Access.UpdateRefreshToken(storedToken);
                await _uow.Access.AddRefreshTokenAsync(newRefreshToken, ct);
                await _uow.CommitAsync(ct);
            }
            catch {
                await _uow.RollbackAsync(ct);
                throw;
            }

            logger.Log($"Token refreshed: {user.Username}", "AUTH-OK");
            var homeAccess = new BranchAccessDto {
                BranchId = user.DefaultBranchId,
                BranchName = user.DefaultBranch?.BranchName ?? string.Empty,
                CanPost = true
            };

            return Result<AuthResponseDto>.Success(new AuthResponseDto {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshRaw,
                ExpiresOn = DateTime.UtcNow.AddMinutes(60),
                UserId = user.Id,
                Username = user.Username,
                FullName = $"{user.FirstName} {user.LastName}".Trim(),
                DefaultBranchId = user.DefaultBranchId,
                Permissions = permissions,
                AccessibleBranches = branchAccess
                    .Where(ba => ba.BranchId != user.DefaultBranchId)
                    .Select(ba => new BranchAccessDto
                    {
                        BranchId = ba.BranchId,
                        BranchName = ba.Branch?.BranchName ?? string.Empty,
                        CanPost = ba.CanPost
                    })
                    .Prepend(homeAccess)
                    .ToList()
            });
        }
    }
}
