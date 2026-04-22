using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Transactions;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class LogoutCommandHandler(IUnitOfWork uow)
    : IRequestHandler<LogoutCommand, Result<bool>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<bool>> Handle(LogoutCommand request, CancellationToken token) {
            return await _uow.ExecuteInTransactionAsync(async token =>
            {
                var tokenEntity = await _uow.Access.GetRefreshTokenAsync(request.RefreshToken, token);

                if (tokenEntity == null)
                    return Result<bool>.Failure("Invalid Token");

                //..check ownership
                if (tokenEntity.UserId != request.UserId)
                    return Result<bool>.Failure("Unauthorized");

                if (tokenEntity.IsRevoked)
                    return Result<bool>.Failure("Token already revoked");

                tokenEntity.IsRevoked = true;
                tokenEntity.UpdatedOn = DateTime.UtcNow;

                _uow.Access.UpdateRefreshToken(tokenEntity);

                return Result<bool>.Success(true);
            }, token);
        }
    }
}
