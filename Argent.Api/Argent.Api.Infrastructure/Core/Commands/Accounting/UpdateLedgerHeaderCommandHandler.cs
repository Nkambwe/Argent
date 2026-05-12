using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects;
using Argent.Api.Infrastructure.Core.Modules.Accounting.RequestObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Accounting {
    public class UpdateLedgerHeaderCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<UpdateLedgerHeaderCommand, Result<LedgerAccountHeaderDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<LedgerAccountHeaderDto>> Handle(UpdateLedgerHeaderCommand command, CancellationToken ct) {
            var header = await _uow.Accounting.GetHeaderByIdAsync(command.HeaderId, ct);
            if (header is null)
                return Result<LedgerAccountHeaderDto>.NotFound("Ledger header not found.");

            //..protect system-managed categories
            if (header.AccountCategory == AccountCategory.None ||
                header.AccountCategory == AccountCategory.Category ||
                header.AccountCategory >= AccountCategory.HeaderTotal)
                return Result<LedgerAccountHeaderDto>.Failure(
                    "This header is system-managed and cannot be modified via the API.",
                    "SYSTEM_MANAGED");

            var req = command.Request;
            header.LedgerName = req.LedgerName;
            header.ParentHeader = req.ParentHeader;
            header.GroupIndex = req.GroupIndex;
            header.LedgerIndex = req.LedgerIndex;
            header.UpdatedBy = _userContext.Username;

            _uow.Accounting.UpdateHeader(header);
            await _uow.CommitAsync(ct);

            return Result<LedgerAccountHeaderDto>.Success(AccountMapper.MapHeaderToDto(header));
        }
    }

}
