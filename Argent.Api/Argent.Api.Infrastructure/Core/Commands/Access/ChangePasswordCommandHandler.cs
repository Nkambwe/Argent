using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class ChangePasswordCommandHandler(IUnitOfWork uow, IServiceLoggerFactory loggerFactory) 
        : IRequestHandler<ChangePasswordCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result> Handle(ChangePasswordCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("access");
            logger.Channel = $"CHANGE-PASSWORD-{command.UserId}";

            var user = await _uow.Users.GetByIdAsync(command.UserId, ct);
            if (user is null)
                return Result.Failure("User not found.", "NOT_FOUND");

            var req = command.Request;

            if (!BCrypt.Net.BCrypt.Verify(req.CurrentPassword, user.PasswordHash)) {
                logger.Log($"Password change failed — wrong current password. UserId: {command.UserId}", "AUTH-FAIL");
                return Result.Failure("Current password is incorrect.", "WRONG_PASSWORD");
            }

            //..check password history, we count from configuration, default is set to 5
            var historyCount = 5;
            var history = await _uow.Users.GetPasswordHistoryAsync(user.Id, historyCount, ct);
            foreach (var prev in history) {
                if (BCrypt.Net.BCrypt.Verify(command.Request.NewPassword, prev.PasswordHash))
                    return Result.Failure($"Password was recently used. Please choose a different password.", "PASSWORD_REUSE");
            }

            return await _uow.ExecuteInTransactionAsync(async token => {
                var newHash = BCrypt.Net.BCrypt.HashPassword(req.NewPassword);
                user.PasswordHash = newHash;
                _uow.Users.Update(user);

                await _uow.Users.AddPasswordHistoryAsync(new PasswordHistory {
                    UserId = user.Id,
                    PasswordHash = newHash,
                    ChangedOn = DateTime.UtcNow
                }, token);

                return Result.Success();
            }, ct);
        }
    }
}
