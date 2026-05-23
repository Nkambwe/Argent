using Argent.Api.Domain.Entities.Products;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Repositories.Products {

    public interface IProductRepository {

        #region Product Types

        Task<IEnumerable<ProductType>> GetProductTypesAsync(ProductModuleType? module = null, CancellationToken ct = default);
        Task<ProductType?> GetProductTypeByIdAsync(long id, CancellationToken ct = default);
        Task<bool> ProductTypeCodeExistsAsync(string code, long? excludeId = null, CancellationToken ct = default);
        Task AddProductTypeAsync(ProductType productType, CancellationToken ct = default);
        void UpdateProductType(ProductType productType);
        void RemoveProductType(ProductType productType);

        #endregion

        #region  Posting Accounts

        Task<IEnumerable<ProductPostingAccount>> GetPostingAccountsAsync(long productId, ProductModuleType module, CancellationToken ct = default);
        Task AddPostingAccountAsync(ProductPostingAccount account, CancellationToken ct = default);
        void UpdatePostingAccount(ProductPostingAccount account);
        void RemovePostingAccount(ProductPostingAccount account);
        Task<ProductPostingAccount?> GetPostingAccountAsync(long productId, ProductModuleType module, PostingPurpose purpose, CustomerSegment segment, CancellationToken ct = default);

        #endregion

        #region  Params

        Task<IEnumerable<ProductParam>> GetParamsAsync(long productId, ProductModuleType module, CancellationToken ct = default);
        Task AddParamAsync(ProductParam param, CancellationToken ct = default);
        void UpdateParam(ProductParam param);

        #endregion

        #region  Saving Products

        Task<IEnumerable<SavingProduct>> GetSavingProductsAsync(bool? isActive = null, CancellationToken ct = default);
        Task<SavingProduct?> GetSavingProductByIdAsync(long id, CancellationToken ct = default);
        Task<bool> ProductCodeExistsAsync(string code, ProductModuleType module, long? excludeId = null, CancellationToken ct = default);
        Task AddSavingProductAsync(SavingProduct product, CancellationToken ct = default);
        void UpdateSavingProduct(SavingProduct product);

        #endregion

        #region Loan Products

        Task<IEnumerable<LoanProduct>> GetLoanProductsAsync(bool? isActive = null, CancellationToken ct = default);
        Task<LoanProduct?> GetLoanProductByIdAsync(long id, CancellationToken ct = default);
        Task AddLoanProductAsync(LoanProduct product, CancellationToken ct = default);
        void UpdateLoanProduct(LoanProduct product);

        #endregion

        #region Share Products

        Task<IEnumerable<ShareProduct>> GetShareProductsAsync(bool? isActive = null, CancellationToken ct = default);
        Task<ShareProduct?> GetShareProductByIdAsync(long id, CancellationToken ct = default);
        Task AddShareProductAsync(ShareProduct product, CancellationToken ct = default);
        void UpdateShareProduct(ShareProduct product);

        #endregion

        #region Time Deposit Products

        Task<IEnumerable<TimedepositProduct>> GetTimedepositProductsAsync(bool? isActive = null, CancellationToken ct = default);
        Task<TimedepositProduct?> GetTimedepositProductByIdAsync(long id, CancellationToken ct = default);
        Task AddTimedepositProductAsync(TimedepositProduct product, CancellationToken ct = default);
        void UpdateTimedepositProduct(TimedepositProduct product);
        Task AddTimedepositRateAsync(TimedepositRate rate, CancellationToken ct = default);
        Task<TimedepositRate?> GetTimedepositRateByIdAsync(long rateId, CancellationToken ct);
        void UpdateTimedepositRate(TimedepositRate rate);
        void RemoveTimedepositRate(TimedepositRate rate);
        Task<TimedepositInterestTier?> GetTimedepositTierByIdAsync(long tierId, CancellationToken ct);
        Task AddTimedepositTierAsync(TimedepositInterestTier tier, CancellationToken ct = default);
        void UpdateTimedepositTier(TimedepositInterestTier tier);
        void RemoveTimedepositTier(TimedepositInterestTier tier);

        #endregion

        #region Insurance Products

        Task<IEnumerable<InsuranceProduct>> GetInsuranceProductsAsync(bool? isActive = null, CancellationToken ct = default);
        Task<InsuranceProduct?> GetInsuranceProductByIdAsync(long id, CancellationToken ct = default);
        Task AddInsuranceProductAsync(InsuranceProduct product, CancellationToken ct = default);
        void UpdateInsuranceProduct(InsuranceProduct product);
       
        #endregion
    }
}
