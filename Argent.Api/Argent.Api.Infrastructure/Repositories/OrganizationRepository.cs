using Argent.Api.Domain.Entities;
using Argent.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Argent.Api.Infrastructure.Repositories {
    public class OrganizationRepository(AppDataContext context) : Repository<Organization>(context), IOrganizationRepository {

        #region Organization

        public async Task<Organization?> GetOrganizationAsync(CancellationToken ct = default)
             => await _dbSet.Include(o => o.Branches.Where(b => !b.IsDeleted)).FirstOrDefaultAsync(ct);

        public async Task<Organization?> GetWithBranchesAsync(long organizationId, CancellationToken token = default)
            => await _dbSet.Include(o => o.Branches.Where(b => !b.IsDeleted))
                .FirstOrDefaultAsync(o => o.Id == organizationId && !o.IsDeleted, token);
        public async Task<bool> RegistrationNumberExistsAsync(string registrationNumber, CancellationToken token = default)
           => await _dbSet.AnyAsync(o => o.RegistrationNumber == registrationNumber && !o.IsDeleted, token);

        #endregion

        #region Branch

        public async Task<IEnumerable<Branch>> GetBranchesAsync(CancellationToken ct = default)
            => await _context.Branches.Include(b => b.Holidays.Where(h => !h.IsDeleted))
                .OrderByDescending(b => b.IsDefault).ThenBy(b => b.BranchName).ToListAsync(ct);

        public async Task<Branch?> GetBranchByIdAsync(long branchId, CancellationToken token = default)
             => await _context.Branches.Include(b => b.Holidays.Where(h => !h.IsDeleted))
            .FirstOrDefaultAsync(b => b.Id == branchId && !b.IsDeleted, token);

        public async Task<IEnumerable<Branch>> GetBranchesByOrganizationAsync(long organizationId, CancellationToken ct = default)
            => await _context.Branches.Where(b => b.OrganizationId == organizationId && !b.IsDeleted)
                .OrderByDescending(b => b.IsDefault)
                .ThenBy(b => b.BranchName)
                .ToListAsync(ct);

        public async Task<Branch?> GetDefaultBranchAsync(long organizationId, CancellationToken token = default)
            => await _context.Branches.FirstOrDefaultAsync(b => b.OrganizationId == organizationId && b.IsDefault && !b.IsDeleted, token);

        public async Task<bool> BranchNameExistsAsync(long organizationId, string branchName, CancellationToken ct = default)
            => await _context.Branches
                .AnyAsync(b => b.OrganizationId == organizationId && b.BranchName == branchName && !b.IsDeleted, ct);

        public async Task<bool> BranchCodeExistsAsync(string code, long? excludeId = null, CancellationToken ct = default)
            => await _context.Branches.AnyAsync(b => b.BranchCode == code && !b.IsDeleted && (excludeId == null || b.Id != excludeId.Value), ct);

        public async Task<bool> HasAnyBranchAsync(CancellationToken token = default)
           => await _context.Branches.AnyAsync(b => !b.IsDeleted, token);

        public async Task AddBranchAsync(Branch branch, CancellationToken ct = default)
            => await _context.Branches.AddAsync(branch, ct);

        public void UpdateBranch(Branch branch) {
            branch.UpdatedOn = DateTime.UtcNow;
            _context.Branches.Update(branch);
        }

        public async Task ClearDefaultBranchAsync(CancellationToken ct = default) {
            var currentDefault = await _context.Branches.FirstOrDefaultAsync(b => b.IsDefault && !b.IsDeleted, ct);

            if (currentDefault is null) return;

            currentDefault.IsDefault = false;
            currentDefault.UpdatedOn = DateTime.UtcNow;
            _context.Branches.Update(currentDefault);
        }

        public async Task ClearAndSetDefaultBranchAsync(long organizationId, long newDefaultBranchId, CancellationToken ct = default) {
            //..remove current default branch as default
            var currentDefaults = await _context.Branches
                .Where(b => b.OrganizationId == organizationId && b.IsDefault && !b.IsDeleted)
                .ToListAsync(ct);

            foreach (var b in currentDefaults) {
                b.IsDefault = false;
                b.UpdatedOn = DateTime.UtcNow;
            }

            //..change to new default branch
            if (newDefaultBranchId != 0) {
                var newDefault = await _context.Branches
                    .FirstOrDefaultAsync(b => b.Id == newDefaultBranchId && !b.IsDeleted, ct)
                    ?? throw new KeyNotFoundException($"Branch {newDefaultBranchId} not found.");

                newDefault.IsDefault = true;
                newDefault.UpdatedOn = DateTime.UtcNow;
            }
        }

        #endregion

        #region Holidays

        public async Task<BranchHoliday?> GetHolidayByIdAsync(long id, CancellationToken ct = default)
            => await _context.BranchHolidays.FirstOrDefaultAsync(h => h.Id == id && !h.IsDeleted, ct);

        public async Task AddHolidayAsync(BranchHoliday holiday, CancellationToken ct = default)
            => await _context.BranchHolidays.AddAsync(holiday, ct);

        public async void RemoveHoliday(BranchHoliday holiday)
           //..hard delete
           => _context.BranchHolidays.Remove(holiday);  

        #endregion

        #region Status
        /// <summary>
        /// Check if we can connect to the database
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CanConnectAsync()
            => await _context.Database.CanConnectAsync();

        /// <summary>
        /// Check if organization is already created
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsInitlialized()
            => await EntityFrameworkQueryableExtensions.AnyAsync(_context.Organizations);

        #endregion

    }
}
