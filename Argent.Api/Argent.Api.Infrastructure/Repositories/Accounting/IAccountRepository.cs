using Argent.Api.Domain.Entities.Accounting;

namespace Argent.Api.Infrastructure.Repositories.Accounting {

    /// <summary>
    /// Repository for Chart of Accounts management.
    /// Covers AccountsChart, LedgerAccountHeader, LedgerAccount, LedgerAccountTotal.
    /// </summary>
    public interface IAccountRepository {
        Task<IEnumerable<AccountsChart>> GetChartsAsync(CancellationToken ct = default);
        Task<AccountsChart?> GetChartByIdAsync(long id, CancellationToken ct = default);
        Task<IEnumerable<LedgerAccountHeader>> GetHeadersAsync(CancellationToken ct = default);
        Task<LedgerAccountHeader?> GetHeaderByIdAsync(long id, CancellationToken ct = default);
        Task<LedgerAccountHeader?> GetHeaderByNumberAsync(string ledgerNumber, CancellationToken ct = default);
        Task AddHeaderAsync(LedgerAccountHeader header, CancellationToken ct = default);
        void UpdateHeader(LedgerAccountHeader header);
        /// <summary>
        /// True if any non-deleted LedgerAccounts exist under this header.
        /// </summary>
        Task<bool> HeaderHasAccountsAsync(long headerId, CancellationToken ct = default);
        /// <summary>
        /// True if any non-deleted child headers reference this header's LedgerNumber as parent.
        /// </summary>
        Task<bool> HeaderHasChildHeadersAsync(string ledgerNumber, CancellationToken ct = default);
        Task<IEnumerable<LedgerAccount>> GetLedgerAccountsAsync(CancellationToken ct = default);
        Task<LedgerAccount?> GetLedgerAccountByIdAsync(long id, CancellationToken ct = default);
        Task<LedgerAccount?> GetLedgerAccountByNumberAsync(string ledgerNumber, CancellationToken ct = default);
        Task AddLedgerAccountAsync(LedgerAccount account, CancellationToken ct = default);
        void UpdateLedgerAccount(LedgerAccount account);
        /// <summary>
        /// True if any GeneralLedgerEntry rows reference this account.
        /// </summary>
        Task<bool> AccountHasTransactionsAsync(long accountId, CancellationToken ct = default);
        /// <summary>
        /// True if a ledger number is already in use across headers or accounts.
        /// </summary>
        Task<bool> LedgerNumberExistsAsync(string ledgerNumber, long? excludeId = null, CancellationToken ct = default);
    }

}
