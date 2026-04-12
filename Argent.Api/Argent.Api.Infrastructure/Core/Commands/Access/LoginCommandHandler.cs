using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Identity;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class LoginCommandHandler(IUnitOfWork uow, IJwtTokenService tokenService, IServiceLoggerFactory loggerFactory) 
        : IRequestHandler<LoginCommand, Result<AuthResponseDto>> {

        private readonly IUnitOfWork _uow = uow;
        private readonly IJwtTokenService _tokenService = tokenService;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        private const int MaxFailedAttempts = 5;
        private const int LockoutMinutes = 15;
        private const int RefreshExpiryDays = 7;

        public async Task<Result<AuthResponseDto>> Handle(LoginCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("access");
            logger.Channel = $"LOGIN-{command.Username}";
            logger.Log($"Login attempt from IP: {command.IpAddress ?? "unknown"}", "AUTH");

            //..find user
            var user = await _uow.Access.GetUserByUsernameAsync(command.Username, ct);
            if (user is null) {
                logger.Log($"Login failed — unknown username: {command.Username}", "AUTH-FAIL");
                // Generic message prevents username enumeration
                return Result<AuthResponseDto>.Failure("Invalid username or password.", "INVALID_CREDENTIALS");
            }

            //.. check if user is active
            if (!user.IsActive) {
                logger.Log($"Login failed — account inactive: {command.Username}", "AUTH-FAIL");
                return Result<AuthResponseDto>.Failure(
                    "Account is inactive. Contact your administrator.", "ACCOUNT_INACTIVE");
            }

            //..check if user is locked
            if (user.LockedUntil.HasValue && user.LockedUntil > DateTime.UtcNow) {
                var remaining = (int)Math.Ceiling((user.LockedUntil.Value - DateTime.UtcNow).TotalMinutes);
                logger.Log($"Login failed — account locked: {command.Username}", "AUTH-FAIL");
                return Result<AuthResponseDto>.Failure(
                    $"Account is locked. Try again in {remaining} minute(s).", "ACCOUNT_LOCKED");
            }

            //..verify password
            if (!BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash)) {
                user.FailedLoginAttempts++;

                if (user.FailedLoginAttempts >= MaxFailedAttempts) {
                    user.LockedUntil = DateTime.UtcNow.AddMinutes(LockoutMinutes);
                    logger.Log(
                        $"Account locked after {MaxFailedAttempts} failed attempts: {command.Username}",
                        "AUTH-FAIL");
                }

                _uow.Access.UpdateUser(user);
                await _uow.CommitAsync(ct);

                return Result<AuthResponseDto>.Failure("Invalid username or password.", "INVALID_CREDENTIALS");
            }

            //..resolve permissions and branch access
            var permissions = await _uow.Access.GetUserPermissionsAsync(user.Id, ct);
            var branchAccess = (await _uow.Access.GetUserBranchAccessAsync(user.Id, ct)).ToList();

            //..generate tokens
            var accessToken = _tokenService.GenerateAccessToken(user, permissions, branchAccess);
            var rawRefresh = _tokenService.GenerateRefreshToken();

            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = rawRefresh,
                ExpiresOn = DateTime.UtcNow.AddDays(RefreshExpiryDays),
                CreatedByIp = command.IpAddress
            };

            //..reset failure counters, record login, persist tokens
            user.FailedLoginAttempts = 0;
            user.LockedUntil = null;
            user.LastLoginOn = DateTime.UtcNow;

            await _uow.BeginTransactionAsync(ct);
            try {
                _uow.Access.UpdateUser(user);
                await _uow.Access.AddRefreshTokenAsync(refreshToken, ct);
                await _uow.CommitAsync(ct);
            } catch {
                await _uow.RollbackAsync(ct);
                throw;
            }

            logger.Log($"Login successful: {command.Username} | HomeBranch: {user.DefaultBranchId}", "AUTH-OK");

            //..build accessible branches list, default branch always included
            var defaultAccess = new BranchAccessDto {
                BranchId = user.DefaultBranchId,
                BranchCode = user.DefaultBranch.BranchCode ?? string.Empty,
                BranchName = user.DefaultBranch?.BranchName ?? string.Empty,
                CanPost = true
            };

            var additionalBranches = branchAccess
                .Where(ba => ba.BranchId != user.DefaultBranchId)
                .Select(ba => new BranchAccessDto
                {
                    BranchId = ba.BranchId,
                    BranchCode = ba.Branch?.BranchCode ?? string.Empty, 
                    BranchName = ba.Branch?.BranchName ?? string.Empty,
                    CanPost = ba.CanPost
                });

            return Result<AuthResponseDto>.Success(new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = rawRefresh,
                ExpiresOn = DateTime.UtcNow.AddMinutes(60),
                UserId = user.Id,
                Username = user.Username,
                FullName = !string.IsNullOrWhiteSpace(user.MiddleName) ? $"{user.FirstName} {user.MiddleName} {user.LastName}".Trim(): $"{user.FirstName} {user.LastName}".Trim(),
                DefaultBranchId = user.DefaultBranchId,
                Permissions = permissions,
                AccessibleBranches = additionalBranches.Prepend(defaultAccess).ToList()
            });
        }

    }
}
