using Argent.Api.Domain.Entities.Accounting;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects;
using Argent.Api.Infrastructure.Core.Modules.Accounting.RequestObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Accounting {
    public class CreateLedgerHeaderCommandHandler
        : IRequestHandler<CreateLedgerHeaderCommand, Result<LedgerAccountHeaderDto>> {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;

        public CreateLedgerHeaderCommandHandler(IUnitOfWork uow, IUserContext userContext) {
            _uow = uow; _userContext = userContext;
        }

        public async Task<Result<LedgerAccountHeaderDto>> Handle(CreateLedgerHeaderCommand command, CancellationToken ct) {
            var req = command.Request;

            //..only SubCategory and Header are allowed via API — Labels and Categories are system-only
            if (req.AccountCategory != AccountCategory.SubCategory &&
                req.AccountCategory != AccountCategory.Header)
                return Result<LedgerAccountHeaderDto>.Failure("Only SubCategory and Header types can be created via the API. " +
                    "Label and Category rows are system-managed.",
                    "INVALID_CATEGORY");

            if (await _uow.Accounting.LedgerNumberExistsAsync(req.LedgerNumber, ct: ct))
                return Result<LedgerAccountHeaderDto>.Failure($"Ledger number '{req.LedgerNumber}' is already in use.", "DUPLICATE_NUMBER");

            //..validate parent exists if provided
            if (!string.IsNullOrWhiteSpace(req.ParentHeader)) {
                var parent = await _uow.Accounting.GetHeaderByNumberAsync(req.ParentHeader, ct);
                if (parent is null)
                    return Result<LedgerAccountHeaderDto>.NotFound($"Parent header '{req.ParentHeader}' not found.");
            }

            var header = new LedgerAccountHeader
            {
                LedgerNumber = req.LedgerNumber,
                LedgerName = req.LedgerName,
                ParentHeader = req.ParentHeader,
                AccountClassification = req.AccountClassification,
                AccountCategory = req.AccountCategory,
                AccountNature = req.AccountNature,
                GroupIndex = req.GroupIndex,
                LedgerIndex = req.LedgerIndex,
                CreatedBy = _userContext.Username
            };

            await _uow.Accounting.AddHeaderAsync(header, ct);
            await _uow.CommitAsync(ct);

            return Result<LedgerAccountHeaderDto>.Success(AccountMapper.MapHeaderToDto(header));
        }
    }

}
