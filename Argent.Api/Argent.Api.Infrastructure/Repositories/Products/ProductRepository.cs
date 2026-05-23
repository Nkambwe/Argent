using Argent.Api.Domain.Entities.Products;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Argent.Api.Infrastructure.Repositories.Products {
    public class ProductRepository(AppDataContext context) : IProductRepository {
        private readonly AppDataContext _context = context;

        #region Product Types

        public async Task<IEnumerable<ProductType>> GetProductTypesAsync(ProductModuleType? module = null, CancellationToken ct = default) {
            var q = _context.ProductTypes.AsQueryable();
            if (module.HasValue)
                q = q.Where(t => t.Module == module.Value);
            return await q.OrderBy(t => t.Module).ThenBy(t => t.Name).ToListAsync(ct);
        }

        public async Task<ProductType?> GetProductTypeByIdAsync(long id, CancellationToken ct = default)
            => await _context.ProductTypes.FirstOrDefaultAsync(
                t => t.Id == id && !t.IsDeleted, ct);

        public async Task<bool> ProductTypeCodeExistsAsync(string code, long? excludeId = null, CancellationToken ct = default)
            => await _context.ProductTypes.AnyAsync(t =>
                t.Code == code &&
                !t.IsDeleted &&
                (excludeId == null || t.Id != excludeId.Value), ct);

        public async Task AddProductTypeAsync(
            ProductType productType, CancellationToken ct = default)
            => await _context.ProductTypes.AddAsync(productType, ct);

        public void UpdateProductType(ProductType productType) {
            productType.UpdatedOn = DateTime.UtcNow;
            _context.ProductTypes.Update(productType);
        }

        public void RemoveProductType(ProductType productType) {
            productType.IsDeleted = true;
            productType.DeletedOn = DateTime.UtcNow;
            _context.ProductTypes.Update(productType);
        }

        #endregion

        #region Posting Accounts

        public async Task<IEnumerable<ProductPostingAccount>> GetPostingAccountsAsync(long productId, ProductModuleType module, CancellationToken ct = default)
            => await _context.ProductPostingAccounts
                .Where(a => a.ProductId == productId &&
                            a.ProductModule == module &&
                            !a.IsDeleted)
                .OrderBy(a => a.PostingPurpose)
                .ThenBy(a => a.CustomerSegment)
                .ToListAsync(ct);

        public async Task AddPostingAccountAsync(
            ProductPostingAccount account, CancellationToken ct = default)
            => await _context.ProductPostingAccounts.AddAsync(account, ct);

        public void UpdatePostingAccount(ProductPostingAccount account) {
            account.UpdatedOn = DateTime.UtcNow;
            _context.ProductPostingAccounts.Update(account);
        }

        public void RemovePostingAccount(ProductPostingAccount account)
            => _context.ProductPostingAccounts.Remove(account);

        public async Task<ProductPostingAccount?> GetPostingAccountAsync(long productId, ProductModuleType module, PostingPurpose purpose, CustomerSegment segment, CancellationToken ct = default)
            => await _context.ProductPostingAccounts
                .FirstOrDefaultAsync(a =>
                    a.ProductId == productId &&
                    a.ProductModule == module &&
                    a.PostingPurpose == purpose &&
                    a.CustomerSegment == segment &&
                    !a.IsDeleted, ct);

        #endregion

        #region Params

        public async Task<IEnumerable<ProductParam>> GetParamsAsync(long productId, ProductModuleType module, CancellationToken ct = default)
            => await _context.ProductParams
                .Where(p => p.ProductId == productId &&
                            p.ProductModule == module &&
                            !p.IsDeleted)
                .OrderBy(p => p.ParameterName)
                .ToListAsync(ct);

        public async Task AddParamAsync(ProductParam param, CancellationToken ct = default)
            => await _context.ProductParams.AddAsync(param, ct);

        public void UpdateParam(ProductParam param) {
            param.UpdatedOn = DateTime.UtcNow;
            _context.ProductParams.Update(param);
        }

        #endregion

        #region Saving Products

        public async Task<IEnumerable<SavingProduct>> GetSavingProductsAsync(bool? isActive = null, CancellationToken ct = default) {
            var q = _context.SavingProducts
                .Include(p => p.ProductType)
                .Include(p => p.Configuration)
                .AsQueryable();
            if (isActive.HasValue)
                q = q.Where(p => p.IsActive == isActive.Value);
            return await q.OrderBy(p => p.ProductName).ToListAsync(ct);
        }

        public async Task<SavingProduct?> GetSavingProductByIdAsync(long id, CancellationToken ct = default)
            => await _context.SavingProducts
                .Include(p => p.ProductType)
                .Include(p => p.Configuration)
                .Include(p => p.TaxGroups)
                .Include(p => p.PostingAccounts.Where(a => !a.IsDeleted))
                .Include(p => p.Params.Where(x => !x.IsDeleted))
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, ct);

        public async Task<bool> ProductCodeExistsAsync(string code, ProductModuleType module,long? excludeId = null, CancellationToken ct = default) {
            // Check across all five product tables — codes must be globally unique
            Func<string, long?, Task<bool>> check = module switch
            {
                ProductModuleType.Savings => async (c, ex) =>
                    await _context.SavingProducts.AnyAsync(p =>
                        p.Code == c && !p.IsDeleted && (ex == null || p.Id != ex.Value), ct),
                ProductModuleType.Loan => async (c, ex) =>
                    await _context.LoanProducts.AnyAsync(p =>
                        p.Code == c && !p.IsDeleted && (ex == null || p.Id != ex.Value), ct),
                ProductModuleType.Share => async (c, ex) =>
                    await _context.ShareProducts.AnyAsync(p =>
                        p.Code == c && !p.IsDeleted && (ex == null || p.Id != ex.Value), ct),
                ProductModuleType.TimeDeposit => async (c, ex) =>
                    await _context.TimedepositProducts.AnyAsync(p =>
                        p.Code == c && !p.IsDeleted && (ex == null || p.Id != ex.Value), ct),
                ProductModuleType.Insurance => async (c, ex) =>
                    await _context.InsuranceProducts.AnyAsync(p =>
                        p.Code == c && !p.IsDeleted && (ex == null || p.Id != ex.Value), ct),
                _ => (_, _) => Task.FromResult(false)
            };
            return await check(code, excludeId);
        }

        public async Task AddSavingProductAsync(SavingProduct product, CancellationToken ct = default)
            => await _context.SavingProducts.AddAsync(product, ct);

        public void UpdateSavingProduct(SavingProduct product) {
            product.UpdatedOn = DateTime.UtcNow;
            _context.SavingProducts.Update(product);
        }

        #endregion

        #region Loan Products

        public async Task<IEnumerable<LoanProduct>> GetLoanProductsAsync(bool? isActive = null, CancellationToken ct = default) {
            var q = _context.LoanProducts
                .Include(p => p.ProductType)
                .Include(p => p.Configuration)
                .AsQueryable();
            if (isActive.HasValue)
                q = q.Where(p => p.IsActive == isActive.Value);
            return await q.OrderBy(p => p.ProductName).ToListAsync(ct);
        }

        public async Task<LoanProduct?> GetLoanProductByIdAsync(long id, CancellationToken ct = default)
            => await _context.LoanProducts
                .Include(p => p.ProductType)
                .Include(p => p.Configuration)
                .Include(p => p.TaxGroups)
                .Include(p => p.PostingAccounts.Where(a => !a.IsDeleted))
                .Include(p => p.Params.Where(x => !x.IsDeleted))
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, ct);

        public async Task AddLoanProductAsync(LoanProduct product, CancellationToken ct = default)
            => await _context.LoanProducts.AddAsync(product, ct);

        public void UpdateLoanProduct(LoanProduct product) {
            product.UpdatedOn = DateTime.UtcNow;
            _context.LoanProducts.Update(product);
        }

        #endregion

        #region Share Products

        public async Task<IEnumerable<ShareProduct>> GetShareProductsAsync(
            bool? isActive = null, CancellationToken ct = default) {
            var q = _context.ShareProducts
                .Include(p => p.ProductType)
                .Include(p => p.Configuration)
                .AsQueryable();
            if (isActive.HasValue)
                q = q.Where(p => p.IsActive == isActive.Value);
            return await q.OrderBy(p => p.ProductName).ToListAsync(ct);
        }

        public async Task<ShareProduct?> GetShareProductByIdAsync(long id, CancellationToken ct = default)
            => await _context.ShareProducts
                .Include(p => p.ProductType)
                .Include(p => p.Configuration)
                .Include(p => p.TaxGroups)
                .Include(p => p.PostingAccounts.Where(a => !a.IsDeleted))
                .Include(p => p.Params.Where(x => !x.IsDeleted))
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, ct);

        public async Task AddShareProductAsync(ShareProduct product, CancellationToken ct = default)
            => await _context.ShareProducts.AddAsync(product, ct);

        public void UpdateShareProduct(ShareProduct product) {
            product.UpdatedOn = DateTime.UtcNow;
            _context.ShareProducts.Update(product);
        }

        #endregion

        #region Time Deposit Products

        public async Task<IEnumerable<TimedepositProduct>> GetTimedepositProductsAsync(
            bool? isActive = null, CancellationToken ct = default) {
            var q = _context.TimedepositProducts
                .Include(p => p.ProductType)
                .Include(p => p.Configuration)
                .Include(p => p.InterestRates.Where(r => r.IsActive))
                .Include(p => p.InterestTiers.Where(t => t.IsActive))
                .AsQueryable();
            if (isActive.HasValue)
                q = q.Where(p => p.IsActive == isActive.Value);
            return await q.OrderBy(p => p.ProductName).ToListAsync(ct);
        }

        public async Task<TimedepositProduct?> GetTimedepositProductByIdAsync(long id, CancellationToken ct = default)
            => await _context.TimedepositProducts
                .Include(p => p.ProductType)
                .Include(p => p.Configuration)
                .Include(p => p.InterestRates)
                .Include(p => p.InterestTiers)
                .Include(p => p.TaxGroups)
                .Include(p => p.PostingAccounts.Where(a => !a.IsDeleted))
                .Include(p => p.Params.Where(x => !x.IsDeleted))
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, ct);

        public async Task AddTimedepositProductAsync(
            TimedepositProduct product, CancellationToken ct = default)
            => await _context.TimedepositProducts.AddAsync(product, ct);

        public void UpdateTimedepositProduct(TimedepositProduct product) {
            product.UpdatedOn = DateTime.UtcNow;
            _context.TimedepositProducts.Update(product);
        }

        public async Task<TimedepositRate?> GetTimedepositRateByIdAsync(long rateId, CancellationToken ct)
            => await _context.TimedepositRates
                .Include(p => p.TimedepositProduct)
                .FirstOrDefaultAsync(p => p.Id == rateId && !p.IsDeleted, ct);

        public async Task AddTimedepositRateAsync(TimedepositRate rate, CancellationToken ct = default)
            => await _context.TimedepositRates.AddAsync(rate, ct);

        public void UpdateTimedepositRate(TimedepositRate rate) {
            rate.UpdatedOn = DateTime.UtcNow;
            _context.TimedepositRates.Update(rate);
        }

        public void RemoveTimedepositRate(TimedepositRate rate)
            => _context.TimedepositRates.Remove(rate);

        public async Task<TimedepositInterestTier?> GetTimedepositTierByIdAsync(long tierId, CancellationToken ct)
            => await _context.TimedepositInterestTiers
                .Include(p => p.TimedepositProduct)
                .FirstOrDefaultAsync(p => p.Id == tierId && !p.IsDeleted, ct);

        public async Task AddTimedepositTierAsync(
            TimedepositInterestTier tier, CancellationToken ct = default)
            => await _context.TimedepositInterestTiers.AddAsync(tier, ct);

        public void UpdateTimedepositTier(TimedepositInterestTier tier) {
            tier.UpdatedOn = DateTime.UtcNow;
            _context.TimedepositInterestTiers.Update(tier);
        }

        public void RemoveTimedepositTier(TimedepositInterestTier tier)
            => _context.TimedepositInterestTiers.Remove(tier);

        #endregion

        #region Insurance Products

        public async Task<IEnumerable<InsuranceProduct>> GetInsuranceProductsAsync(
            bool? isActive = null, CancellationToken ct = default) {
            var q = _context.InsuranceProducts
                .Include(p => p.ProductType)
                .Include(p => p.Configuration)
                .AsQueryable();
            if (isActive.HasValue)
                q = q.Where(p => p.IsActive == isActive.Value);
            return await q.OrderBy(p => p.ProductName).ToListAsync(ct);
        }

        public async Task<InsuranceProduct?> GetInsuranceProductByIdAsync(long id, CancellationToken ct = default)
            => await _context.InsuranceProducts
                .Include(p => p.ProductType)
                .Include(p => p.Configuration)
                .Include(p => p.TaxGroups)
                .Include(p => p.PostingAccounts.Where(a => !a.IsDeleted))
                .Include(p => p.Params.Where(x => !x.IsDeleted))
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, ct);

        public async Task AddInsuranceProductAsync(InsuranceProduct product, CancellationToken ct = default)
            => await _context.InsuranceProducts.AddAsync(product, ct);

        public void UpdateInsuranceProduct(InsuranceProduct product) {
            product.UpdatedOn = DateTime.UtcNow;
            _context.InsuranceProducts.Update(product);
        }

        #endregion
    }
}
