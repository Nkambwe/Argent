using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class UpdateSavingProductConfigCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<UpdateSavingProductConfigCommand, Result<SavingProductDetailDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<SavingProductDetailDto>> Handle(UpdateSavingProductConfigCommand command, CancellationToken ct) {
            var p = await _uow.Products.GetSavingProductByIdAsync(command.Id, ct);
            if (p is null) return Result<SavingProductDetailDto>.NotFound("Saving product not found.");
            if (p.Configuration is null)
                return Result<SavingProductDetailDto>.Failure("Product configuration not found.", "NO_CONFIG");

            var req = command.Request;
            var cfg = p.Configuration;
            var logger = _loggerFactory.CreateLogger("kyc");
            logger.Channel = $"SAVING-PRODUCT-{command.Id}-CONFIGURATIONS";

            cfg.InterestBasedProduct = req.InterestBasedProduct;
            cfg.InterestRate = req.InterestRate;
            cfg.InterestDays = req.InterestDays;
            cfg.InterestWeeks = req.InterestWeeks;
            cfg.InterestMethod = req.InterestMethod;
            cfg.OfferInterestOnDormantAccounts = req.OfferInterestOnDormantAccounts;
            cfg.ChargeWithholdingTaxOnSavingInterest = req.ChargeWithholdingTaxOnSavingInterest;
            cfg.TurnOnOverdraftProtection = req.TurnOnOverdraftProtection;
            cfg.OverdraftPeriod = req.OverdraftPeriod;
            cfg.OverdraftInterestRate = req.OverdraftInterestRate;
            cfg.ChargeCommissionOnOverdraft = req.ChargeCommissionOnOverdraft;
            cfg.ChargeInterestOnNegativeBalances = req.ChargeInterestOnNegativeBalances;
            cfg.NegativeBalanceInterestRate = req.NegativeBalanceInterestRate;
            cfg.MinimumInterestOnNegativeBalance = req.MinimumInterestOnNegativeBalance;
            cfg.ConsiderDormantAfterDaysOfInactivity = req.ConsiderDormantAfterDaysOfInactivity;
            cfg.RequireSupervisorApprovalToActivateDormantAccounts = req.RequireSupervisorApprovalToActivateDormantAccounts;
            cfg.RequireApprovalForWithdraws = req.RequireApprovalForWithdraws;
            cfg.RequireApprovalForWithdrawsAboveCashierLimit = req.RequireApprovalForWithdrawsAboveCashierLimit;
            cfg.ChargeWithdrawCommission = req.ChargeWithdrawCommission;
            cfg.UseWithdrawCommissionRange = req.UseWithdrawCommissionRange;
            cfg.WithdrawInterval = req.WithdrawInterval;
            cfg.ChargePenaltyForWithdrawInterval = req.ChargePenaltyForWithdrawInterval;
            cfg.HasChequeBook = req.HasChequeBook;
            cfg.NumberOfLeafs = req.NumberOfLeafs;
            cfg.ChargePerLeaf = req.ChargePerLeaf;
            cfg.ChequeBookCharge = req.ChequeBookCharge;
            cfg.AllowChequeDeposit = req.AllowChequeDeposit;
            cfg.AutoExecuteStandingOrdersAtStartOfDay = req.AutoExecuteStandingOrdersAtStartOfDay;
            cfg.EnableSmsBanking = req.EnableSmsBanking;
            cfg.MaximumAmountPerSmsTransaction = req.MaximumAmountPerSmsTransaction;
            cfg.EnableElectronicCardTransaction = req.EnableElectronicCardTransaction;
            cfg.ElectronicCardWithdrawLimit = req.ElectronicCardWithdrawLimit;
            cfg.ElectronicCardPurchaseLimit = req.ElectronicCardPurchaseLimit;
            cfg.BookSavingsToGeneralLedger = req.BookSavingsToGeneralLedger;
            cfg.AllowMultiCurrency = req.AllowMultiCurrency;
            cfg.EnforceIndividualSaving = req.EnforceIndividualSaving;
            cfg.MinimumBalanceIndividualAccounts = req.MinimumBalanceIndividualAccounts;
            cfg.MinimumInterestEarningBalanceIndividualAccounts = req.MinimumInterestEarningBalanceIndividualAccounts;
            cfg.EnforceGroupSaving = req.EnforceGroupSaving;
            cfg.BreakGroupAccountsToIndividualMemberAccounts = req.BreakGroupAccountsToIndividualMemberAccounts;
            cfg.MinimumBalanceGroupAccounts = req.MinimumBalanceGroupAccounts;
            cfg.MinimumInterestEarningBalanceGroupAccounts = req.MinimumInterestEarningBalanceGroupAccounts;
            cfg.EnforceBusinessSaving = req.EnforceBusinessSaving;
            cfg.MinimumBalanceBusinessAccounts = req.MinimumBalanceBusinessAccounts;
            cfg.MinimumInterestEarningBalanceBusinessAccounts = req.MinimumInterestEarningBalanceBusinessAccounts;
            cfg.ChargeAccountOpeningFees = req.ChargeAccountOpeningFees;
            cfg.ChargeAccountClosureFees = req.ChargeAccountClosureFees;
            cfg.ChargeSavingsTransferFees = req.ChargeSavingsTransferFees;
            cfg.MinimumClientAge = req.MinimumClientAge;
            cfg.UpdatedBy = _userContext.Username;

            _uow.Products.UpdateSavingProduct(p);
            await _uow.CommitAsync(ct);

            logger.Log($"Saving Product Configuration for Product ID {p.Code} added", "PDT-OK");
            var updated = await _uow.Products.GetSavingProductByIdAsync(p.Id, ct);
            return Result<SavingProductDetailDto>.Success(ProductMapper.MapSavingDetail(updated!));
        }
    }
}
