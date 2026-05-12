using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Accounting {
    public class ActivateLedgerAccountCommandHandler
        : IRequestHandler<ActivateLedgerAccountCommand, Result> {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;

        public ActivateLedgerAccountCommandHandler(IUnitOfWork uow, IUserContext userContext) {
            _uow = uow; _userContext = userContext;
        }

        public async Task<Result> Handle(
            ActivateLedgerAccountCommand command, CancellationToken ct) {
            var account = await _uow.Accounting.GetLedgerAccountByIdAsync(command.AccountId, ct);
            if (account is null)
                return Result.Failure("Ledger account not found.", "NOT_FOUND");

            if (!account.Suspended)
                return Result.Failure("Account is not suspended.", "NOT_SUSPENDED");

            account.Suspended = false;
            account.UpdatedBy = _userContext.Username;
            _uow.Accounting.UpdateLedgerAccount(account);
            await _uow.CommitAsync(ct);

            return Result.Success();
        }
    }

}
