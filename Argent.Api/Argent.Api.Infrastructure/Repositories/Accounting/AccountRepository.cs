using Argent.Api.Domain.Entities.Accounting;
using Argent.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Argent.Api.Infrastructure.Repositories.Accounting {
    public class AccountRepository(AppDataContext context) : IAccountRepository {
        private readonly AppDataContext _context = context;

        public async Task<IEnumerable<AccountsChart>> GetChartsAsync(CancellationToken ct = default)
            => await _context.AccountsCharts.Include(c => c.LedgerAccounts.Where(a => !a.IsDeleted))
                .OrderBy(c => c.ChartName)
                .ToListAsync(ct);

        public async Task<AccountsChart?> GetChartByIdAsync(long id, CancellationToken ct = default)
            => await _context.AccountsCharts.Include(c => c.LedgerAccounts.Where(a => !a.IsDeleted))
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted, ct);

        public async Task<IEnumerable<LedgerAccountHeader>> GetHeadersAsync(CancellationToken ct = default)
            => await _context.LedgerAccountHeaders.Include(h => h.LedgerAccounts.Where(a => !a.IsDeleted))
                .Include(h => h.TotalLabels.Where(t => !t.IsDeleted))
                .OrderBy(h => h.LedgerIndex)
                .ToListAsync(ct);

        public async Task<LedgerAccountHeader?> GetHeaderByIdAsync(long id, CancellationToken ct = default)
            => await _context.LedgerAccountHeaders.Include(h => h.LedgerAccounts.Where(a => !a.IsDeleted))
                .Include(h => h.TotalLabels.Where(t => !t.IsDeleted))
                .FirstOrDefaultAsync(h => h.Id == id && !h.IsDeleted, ct);

        public async Task<LedgerAccountHeader?> GetHeaderByNumberAsync(string ledgerNumber, CancellationToken ct = default)
            => await _context.LedgerAccountHeaders.FirstOrDefaultAsync(h => h.LedgerNumber == ledgerNumber && !h.IsDeleted, ct);

        public async Task AddHeaderAsync( LedgerAccountHeader header, CancellationToken ct = default)
            => await _context.LedgerAccountHeaders.AddAsync(header, ct);

        public void UpdateHeader(LedgerAccountHeader header) {
            header.UpdatedOn = DateTime.UtcNow;
            _context.LedgerAccountHeaders.Update(header);
        }

        public async Task<bool> HeaderHasAccountsAsync(long headerId, CancellationToken ct = default)
            => await _context.LedgerAccounts.AnyAsync(a => a.LedgerAccountHeaderId == headerId && !a.IsDeleted, ct);

        public async Task<bool> HeaderHasChildHeadersAsync(string ledgerNumber, CancellationToken ct = default)
            => await _context.LedgerAccountHeaders
                .AnyAsync(h => h.ParentHeader == ledgerNumber && !h.IsDeleted, ct);

        public async Task<IEnumerable<LedgerAccount>> GetLedgerAccountsAsync(CancellationToken ct = default)
            => await _context.LedgerAccounts.Include(a => a.LedgerAccountHeader).OrderBy(a => a.LedgerIndex).ToListAsync(ct);

        public async Task<LedgerAccount?> GetLedgerAccountByIdAsync(long id, CancellationToken ct = default)
            => await _context.LedgerAccounts.Include(a => a.LedgerAccountHeader).FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted, ct);

        public async Task<LedgerAccount?> GetLedgerAccountByNumberAsync(string ledgerNumber, CancellationToken ct = default)
            => await _context.LedgerAccounts.Include(a => a.LedgerAccountHeader).FirstOrDefaultAsync(a => a.LedgerNumber == ledgerNumber && !a.IsDeleted, ct);

        public async Task AddLedgerAccountAsync(LedgerAccount account, CancellationToken ct = default)
            => await _context.LedgerAccounts.AddAsync(account, ct);

        public void UpdateLedgerAccount(LedgerAccount account) {
            account.UpdatedOn = DateTime.UtcNow;
            _context.LedgerAccounts.Update(account);
        }

        public async Task<bool> AccountHasTransactionsAsync(long accountId, CancellationToken ct = default)
            => await _context.GeneralLedgerEntries.AnyAsync(e => e.LedgerAccountId == accountId, ct);

        public async Task<bool> LedgerNumberExistsAsync(string ledgerNumber, long? excludeId = null, CancellationToken ct = default) {
            //..check both headers and accounts — ledger numbers must be unique across both
            var inHeaders = await _context.LedgerAccountHeaders
                .AnyAsync(h =>h.LedgerNumber == ledgerNumber && !h.IsDeleted && (excludeId == null || h.Id != excludeId.Value), ct);

            if (inHeaders) return true;

            return await _context.LedgerAccounts.AnyAsync(a => a.LedgerNumber == ledgerNumber &&
                        !a.IsDeleted && (excludeId == null || a.Id != excludeId.Value), ct);
        }
    }

}
