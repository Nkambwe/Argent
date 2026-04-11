using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result> {
        private readonly IUnitOfWork _uow;
        private readonly IServiceLoggerFactory _loggerFactory;

        public ChangePasswordCommandHandler(IUnitOfWork uow, IServiceLoggerFactory loggerFactory) {
            _uow = uow;
            _loggerFactory = loggerFactory;
        }

        public async Task<Result> Handle(ChangePasswordCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("access");
            logger.Channel = $"CHANGE-PASSWORD-{command.UserId}";

            var user = await _uow.Access.GetByIdAsync(command.UserId, ct);
            if (user is null)
                return Result.Failure("User not found.", "NOT_FOUND");

            if (!BCrypt.Net.BCrypt.Verify(command.CurrentPassword, user.PasswordHash)) {
                logger.Log($"Password change failed — wrong current password. UserId: {command.UserId}", "AUTH-FAIL");
                return Result.Failure("Current password is incorrect.", "WRONG_PASSWORD");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.NewPassword, workFactor: 12);
            _uow.Access.UpdateUser(user);
            await _uow.CommitAsync(ct);

            logger.Log($"Password changed successfully. UserId: {command.UserId}", "INFO");
            return Result.Success();
        }
    }
}
