using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Identity;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class LoginCommandHandler(
        IUnitOfWork uow,
        IJwtTokenService jwtService,
        IServiceLoggerFactory loggerFactory) : IRequestHandler<LoginCommand, Result<AuthResponse>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IJwtTokenService _jwtService = jwtService;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<AuthResponse>> Handle(LoginCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("auth");
            logger.Channel = $"LOGIN-{command.Username}";
            logger.Log($"Login attempt from IP {command.IpAddress}", "AUTH");

            var user = await _uow.Access.GetUserByUsernameAsync(command.Username, ct);

            // Deliberate: same error for wrong username or wrong password (prevents enumeration)
            if (user is null) {
                logger.Log($"Login failed — username not found: {command.Username}", "AUTH-FAIL");
                return Result<AuthResponse>.Failure("Invalid username or password.", "INVALID_CREDENTIALS");
            }

            // Account lockout check
            if (user.LockedUntil.HasValue && user.LockedUntil > DateTime.UtcNow) {
                logger.Log($"Login blocked — account locked until {user.LockedUntil:u}", "AUTH-FAIL");
                return Result<AuthResponse>.Failure(
                    $"Account is locked. Try again after {user.LockedUntil:HH:mm UTC}.",
                    "ACCOUNT_LOCKED");
            }

            if (!BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash)) {
                user.FailedLoginAttempts++;

                // Lock after 5 failed attempts — 15 minute lockout
                if (user.FailedLoginAttempts >= 5) {
                    user.LockedUntil = DateTime.UtcNow.AddMinutes(15);
                    logger.Log($"Account locked after {user.FailedLoginAttempts} failed attempts", "AUTH-FAIL");
                }

                _uow.Access.UpdateUser(user);
                await _uow.CommitAsync(ct);

                return Result<AuthResponse>.Failure("Invalid username or password.", "INVALID_CREDENTIALS");
            }

            // Credentials valid — load full permissions
            var permissions = await _uow.Access.GetPermissionsByUserAsync(user.Id, ct);
            var permissionNames = permissions.Select(p => p.Name).ToList();

            // Generate tokens
            var accessToken = _jwtService.GenerateAccessToken(user, permissionNames);
            var refreshToken = _jwtService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedByIp = command.IpAddress
            };

            // Update user login metadata
            user.LastLoginAt = DateTime.UtcNow;
            user.FailedLoginAttempts = 0;
            user.LockedUntil = null;

            await _uow.Access.AddRefreshTokenAsync(refreshTokenEntity, ct);
            _uow.Access.UpdateUser(user);
            await _uow.CommitAsync(ct);

            logger.Log($"Login successful for user {user.Username}", "AUTH");

            var jwtExpiry = DateTime.UtcNow.AddMinutes(60); // mirrors JwtSettings

            return Result<AuthResponse>.Success(new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiry = jwtExpiry,
                User = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    DefaultBranchId = user.DefaultBranchId,
                    HomeBranchName = user.DefaultBranch?.BranchName ?? string.Empty,
                    Permissions = permissionNames,
                    IsActive = user.IsActive,
                    LastLoginAt = user.LastLoginAt
                }
            });
        }
    }
}
