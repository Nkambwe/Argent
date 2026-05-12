using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Accounting {
    public class SuspendLedgerAccountCommandHandler
        : IRequestHandler<SuspendLedgerAccountCommand, Result> {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;

        public SuspendLedgerAccountCommandHandler(IUnitOfWork uow, IUserContext userContext) {
            _uow = uow; _userContext = userContext;
        }

        public async Task<Result> Handle(SuspendLedgerAccountCommand command, CancellationToken ct) {
            var account = await _uow.Accounting.GetLedgerAccountByIdAsync(command.AccountId, ct);
            if (account is null)
                return Result.Failure("Ledger account not found.", "NOT_FOUND");

            if (account.Suspended)
                return Result.Failure("Account is already suspended.", "ALREADY_SUSPENDED");

            account.Suspended = true;
            account.UpdatedBy = _userContext.Username;
            _uow.Accounting.UpdateLedgerAccount(account);
            await _uow.CommitAsync(ct);

            return Result.Success();
        }
    }

}
