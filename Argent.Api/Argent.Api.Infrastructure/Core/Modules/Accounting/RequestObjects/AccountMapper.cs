using Argent.Api.Domain.Entities.Accounting;
using Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects;

namespace Argent.Api.Infrastructure.Core.Modules.Accounting.RequestObjects {
    public static class AccountMapper {
        public static LedgerAccountHeaderDto MapHeaderToDto(LedgerAccountHeader h) => new()
        {
            Id = h.Id,
            LedgerNumber = h.LedgerNumber,
            LedgerName = h.LedgerName,
            ParentHeader = h.ParentHeader,
            AccountClassification = h.AccountClassification.ToString(),
            AccountCategory = h.AccountCategory.ToString(),
            AccountNature = h.AccountNature.ToString(),
            GroupIndex = h.GroupIndex,
            LedgerIndex = h.LedgerIndex,
            LedgerAccountCount = h.LedgerAccounts.Count(a => !a.IsDeleted)
        };

        public static LedgerAccountDto MapAccountToDto(LedgerAccount a) => new()
        {
            Id = a.Id,
            LedgerAccountHeaderId = a.LedgerAccountHeaderId,
            HeaderName = a.LedgerAccountHeader?.LedgerName ?? string.Empty,
            AccountsChartId = a.AccountsChartId,
            LedgerNumber = a.LedgerNumber,
            LedgerName = a.LedgerName,
            AccountClassification = a.AccountClassification.ToString(),
            AccountNature = a.AccountNature.ToString(),
            NormalBalance = a.NormalBalance.ToString(),
            PostingType = a.PostingType.ToString(),
            AllowManualPosting = a.AllowManualPosting,
            ShowParticulars = a.ShowParticulars,
            Suspended = a.Suspended,
            Balance = a.Balance,
            Notes = a.Notes,
            GroupIndex = a.GroupIndex,
            LedgerIndex = a.LedgerIndex
        };
    }

}
