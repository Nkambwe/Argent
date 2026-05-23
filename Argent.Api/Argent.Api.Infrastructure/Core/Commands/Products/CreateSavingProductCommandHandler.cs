using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Products;
using Argent.Api.Domain.Entities.Settings;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.OverrideObjects;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;
using System.Reflection;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class CreateSavingProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<CreateSavingProductCommand, Result<SavingProductDetailDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<SavingProductDetailDto>> Handle(CreateSavingProductCommand command, CancellationToken ct) {
            var req = command.Request;
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"SAVING-PRODUCTS-{req.Code.ToUpper()}";

            if (await _uow.Products.ProductCodeExistsAsync(req.Code, ProductModuleType.Savings, ct: ct))
                return Result<SavingProductDetailDto>.Failure($"Product code '{req.Code}' already exists.", "DUPLICATE_CODE");

            var productType = await _uow.Products.GetProductTypeByIdAsync(req.ProductTypeId, ct);
            if (productType is null)
                return Result<SavingProductDetailDto>.NotFound("Product type not found.");
            if (productType.Module != ProductModuleType.Savings)
                return Result<SavingProductDetailDto>.Failure("Product type does not belong to the Savings module.", "WRONG_MODULE");

            return await _uow.ExecuteInTransactionAsync(async token =>
            {
                var product = new SavingProduct
                {
                    Code = req.Code,
                    ProductName = req.ProductName,
                    Description = req.Description,
                    ProductTypeId = req.ProductTypeId,
                    ChargeGroupId = req.ChargeGroupId,
                    VatInclusive = req.VatInclusive,
                    UseChargeGroups = req.UseChargeGroups,
                    LimitWithdraw = req.LimitWithdraw,
                    MaximumWithdraws = req.MaximumWithdraws,
                    WithdrawPenalty = req.WithdrawPenalty,
                    ChargeWithdraws = req.ChargeWithdraws,
                    AllowOverdraft = req.AllowOverdraft,
                    OverdraftInterest = req.OverdraftInterest,
                    MinimumBalance = req.MinimumBalance,
                    OfferInterest = req.OfferInterest,
                    InterestRate = req.InterestRate,
                    MinimumInterestOffered = req.MinimumInterestOffered,
                    IsActive = true,
                    CreatedBy = _userContext.Username
                };

                await _uow.Products.AddSavingProductAsync(product, token);
                //..flush to get Id
                await _uow.CommitAsync(token);

                logger.Log($"Saving Product Registered: {product.Code} — {product.ProductName}", "PDT-OK");

                //..seed configuration from defaults, applying any overrides
                var config = SeedSavingConfiguration(product.Id, req.ConfigOverrides, _userContext.Username);
                product.Configuration = config;
                _uow.Products.UpdateSavingProduct(product);

                //..seed ProductParam records from ConfigurationParamAttribute attributes
                await SeedProductParamsAsync(product.Id, ProductModuleType.Savings, config, token);

                //..posting accounts
                foreach (var pa in req.PostingAccounts)
                    await _uow.Products.AddPostingAccountAsync(new ProductPostingAccount {
                        ProductId = product.Id,
                        ProductModule = ProductModuleType.Savings,
                        PostingPurpose = pa.PostingPurpose,
                        CustomerSegment = pa.CustomerSegment,
                        LedgerNumber = pa.LedgerNumber,
                        CostCentreCode = pa.CostCentreCode,
                        RevenueCentreCode = pa.RevenueCentreCode,
                        CreatedBy = _userContext.Username
                    }, token);

                var created = await _uow.Products.GetSavingProductByIdAsync(product.Id, token);
                return Result<SavingProductDetailDto>.Success(ProductMapper.MapSavingDetail(created!));
            }, ct);
        }

        private static SavingProductConfiguration SeedSavingConfiguration(long productId, SavingProductConfigOverrides? overrides, string createdBy) {
            return new SavingProductConfiguration
            {
                SavingProductId = productId,
                InterestBasedProduct = overrides?.InterestBasedProduct ?? false,
                InterestRate = overrides?.InterestRate ?? 0,
                InterestDays = 365,
                InterestWeeks = 52,
                InterestMethod = overrides?.InterestMethod ?? SavingInterestCalculation.DailyBalance,
                OfferInterestOnDormantAccounts = false,
                ChargeWithholdingTaxOnSavingInterest = false,
                TurnOnOverdraftProtection = overrides?.TurnOnOverdraftProtection ?? true,
                OverdraftPeriod = overrides?.OverdraftPeriod ?? 30,
                OverdraftInterestRate = overrides?.OverdraftInterestRate ?? 0,
                ChargeCommissionOnOverdraft = false,
                ChargeInterestOnNegativeBalances = false,
                ConsiderDormantAfterDaysOfInactivity = overrides?.ConsiderDormantAfterDaysOfInactivity ?? 365,
                BookSavingsToGeneralLedger = overrides?.BookSavingsToGeneralLedger ?? true,
                AllowMultiCurrency = false,
                EnforceIndividualSaving = false,
                MinimumBalanceIndividualAccounts = overrides?.MinimumBalanceIndividualAccounts ?? 0,
                MinimumInterestEarningBalanceIndividualAccounts = 0,
                EnforceGroupSaving = false,
                MinimumBalanceGroupAccounts = overrides?.MinimumBalanceGroupAccounts ?? 0,
                EnforceBusinessSaving = false,
                MinimumBalanceBusinessAccounts = overrides?.MinimumBalanceBusinessAccounts ?? 0,
                MinimumClientAge = overrides?.MinimumClientAge ?? 18,
                CreatedBy = createdBy
            };
        }

        private async Task SeedProductParamsAsync(long productId, ProductModuleType module, object configObject, CancellationToken ct) {
            var props = configObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetCustomAttribute<ConfigurationParamAttribute>() is not null);

            foreach (var prop in props) {
                var attr = prop.GetCustomAttribute<ConfigurationParamAttribute>()!;
                var value = prop.GetValue(configObject);

                await _uow.Products.AddParamAsync(new ProductParam {
                    ProductId = productId,
                    ProductModule = module,
                    ParameterName = attr.Name,
                    ParamValue = value?.ToString() ?? string.Empty,
                    DataType = attr.ParamType,
                    Description = attr.Description,
                    IsEditable = true,
                    CreatedBy = _userContext.Username
                }, ct);
            }
        }
    }
}
