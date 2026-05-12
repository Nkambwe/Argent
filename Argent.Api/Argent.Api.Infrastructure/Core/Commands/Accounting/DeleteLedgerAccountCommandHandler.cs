using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Accounting {
    public class DeleteLedgerAccountCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<DeleteLedgerAccountCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result> Handle(DeleteLedgerAccountCommand command, CancellationToken ct) {
            var account = await _uow.Accounting.GetLedgerAccountByIdAsync(command.AccountId, ct);
            if (account is null)
                return Result.Failure("Ledger account not found.", "NOT_FOUND");

            //..block deletion if balance is non-zero
            if (account.Balance != 0)
                return Result.Failure($"Cannot delete account — current balance is {account.Balance:N2}. " +
                    "Clear the balance first or suspend the account instead.", "NON_ZERO_BALANCE");

            //..block deletion if any GL transactions reference this account
            var hasTransactions = await _uow.Accounting.AccountHasTransactionsAsync(command.AccountId, ct);
            if (hasTransactions)
                return Result.Failure("Cannot delete account — GL transactions exist on this account. " +
                    "Suspend the account instead.", "HAS_TRANSACTIONS");

            account.IsDeleted = true;
            account.DeletedOn = DateTime.UtcNow;
            account.DeletedBy = _userContext.Username;
            _uow.Accounting.UpdateLedgerAccount(account);
            await _uow.CommitAsync(ct);

            return Result.Success();
        }
    }

}
