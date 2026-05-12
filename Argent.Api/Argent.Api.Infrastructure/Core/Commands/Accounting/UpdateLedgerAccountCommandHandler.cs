using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects;
using Argent.Api.Infrastructure.Core.Modules.Accounting.RequestObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Accounting {
    public class UpdateLedgerAccountCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<UpdateLedgerAccountCommand, Result<LedgerAccountDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<LedgerAccountDto>> Handle(
            UpdateLedgerAccountCommand command, CancellationToken ct) {
            var account = await _uow.Accounting.GetLedgerAccountByIdAsync(command.AccountId, ct);
            if (account is null)
                return Result<LedgerAccountDto>.NotFound("Ledger account not found.");

            var req = command.Request;

            // Changing header: only allowed if no transactions exist
            if (account.LedgerAccountHeaderId != req.LedgerAccountHeaderId) {
                var hasTransactions = await _uow.Accounting.AccountHasTransactionsAsync(command.AccountId, ct);
                if (hasTransactions)
                    return Result<LedgerAccountDto>.Failure(
                        "Cannot move account to a different header — transactions already exist on this account.",
                        "HAS_TRANSACTIONS");

                var newHeader = await _uow.Accounting.GetHeaderByIdAsync(req.LedgerAccountHeaderId, ct);
                if (newHeader is null)
                    return Result<LedgerAccountDto>.NotFound("Target ledger header not found.");

                account.LedgerAccountHeaderId = req.LedgerAccountHeaderId;
            }

            account.LedgerName = req.LedgerName;
            account.NormalBalance = req.NormalBalance;
            account.PostingType = req.PostingType;
            account.AllowManualPosting = req.AllowManualPosting;
            account.ShowParticulars = req.ShowParticulars;
            account.Notes = req.Notes;
            account.GroupIndex = req.GroupIndex;
            account.LedgerIndex = req.LedgerIndex;
            account.UpdatedBy = _userContext.Username;

            _uow.Accounting.UpdateLedgerAccount(account);
            await _uow.CommitAsync(ct);

            return Result<LedgerAccountDto>.Success(AccountMapper.MapAccountToDto(account));
        }
    }

}
