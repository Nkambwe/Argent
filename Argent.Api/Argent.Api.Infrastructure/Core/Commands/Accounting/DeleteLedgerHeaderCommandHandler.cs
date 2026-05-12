using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Accounting {
    public class DeleteLedgerHeaderCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<DeleteLedgerHeaderCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result> Handle(DeleteLedgerHeaderCommand command, CancellationToken ct) {
            var header = await _uow.Accounting.GetHeaderByIdAsync(command.HeaderId, ct);
            if (header is null)
                return Result.Failure("Ledger header not found.", "NOT_FOUND");

            if (header.AccountCategory == AccountCategory.None ||
                header.AccountCategory == AccountCategory.Category ||
                header.AccountCategory >= AccountCategory.HeaderTotal)
                return Result.Failure(
                    "System-managed headers cannot be deleted.", "SYSTEM_MANAGED");

            //..block deletion if child accounts exist
            var hasAccounts = await _uow.Accounting.HeaderHasAccountsAsync(command.HeaderId, ct);
            if (hasAccounts)
                return Result.Failure("Cannot delete header — it has active ledger accounts. " +
                    "Suspend or delete all child accounts first.", "HAS_ACCOUNTS");

            //..block if child headers exist
            var hasChildHeaders = await _uow.Accounting.HeaderHasChildHeadersAsync(
                header.LedgerNumber, ct);
            if (hasChildHeaders)
                return Result.Failure("Cannot delete header — it has child headers. " +
                    "Delete child headers first.", "HAS_CHILDREN");

            header.IsDeleted = true;
            header.DeletedOn = DateTime.UtcNow;
            header.DeletedBy = _userContext.Username;
            _uow.Accounting.UpdateHeader(header);
            await _uow.CommitAsync(ct);

            return Result.Success();
        }
    }

}
