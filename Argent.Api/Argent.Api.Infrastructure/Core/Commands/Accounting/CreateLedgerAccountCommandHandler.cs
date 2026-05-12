using Argent.Api.Domain.Entities.Accounting;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects;
using Argent.Api.Infrastructure.Core.Modules.Accounting.RequestObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Accounting {
    public class CreateLedgerAccountCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<CreateLedgerAccountCommand, Result<LedgerAccountDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<LedgerAccountDto>> Handle(CreateLedgerAccountCommand command, CancellationToken ct) {
            var req = command.Request;

            if (await _uow.Accounting.LedgerNumberExistsAsync(req.LedgerNumber, ct: ct))
                return Result<LedgerAccountDto>.Failure($"Ledger number '{req.LedgerNumber}' is already in use.", "DUPLICATE_NUMBER");

            var header = await _uow.Accounting.GetHeaderByIdAsync(req.LedgerAccountHeaderId, ct);
            if (header is null)
                return Result<LedgerAccountDto>.NotFound("Ledger header not found.");

            // Only Header-category parents can contain postable accounts
            if (header.AccountCategory != AccountCategory.Header &&
                header.AccountCategory != AccountCategory.SubCategory)
                return Result<LedgerAccountDto>.Failure("Ledger accounts must be placed under a Header or SubCategory.", "INVALID_PARENT");

            var chart = await _uow.Accounting.GetChartByIdAsync(req.AccountsChartId, ct);
            if (chart is null)
                return Result<LedgerAccountDto>.NotFound("Chart of accounts not found.");

            var account = new LedgerAccount
            {
                LedgerAccountHeaderId = req.LedgerAccountHeaderId,
                AccountsChartId = req.AccountsChartId,
                LedgerNumber = req.LedgerNumber,
                LedgerName = req.LedgerName,
                AccountClassification = req.AccountClassification,
                AccountCategory = AccountCategory.Ledger,    
                AccountNature = req.AccountNature,
                NormalBalance = req.NormalBalance,
                PostingType = req.PostingType,
                AllowManualPosting = req.AllowManualPosting,
                ShowParticulars = req.ShowParticulars,
                Suspended = false,
                Balance = 0.00m,
                Notes = req.Notes,
                GroupIndex = req.GroupIndex,
                LedgerIndex = req.LedgerIndex,
                CreatedBy = _userContext.Username
            };

            await _uow.Accounting.AddLedgerAccountAsync(account, ct);
            await _uow.CommitAsync(ct);

            return Result<LedgerAccountDto>.Success(AccountMapper.MapAccountToDto(account));
        }
    }

}
