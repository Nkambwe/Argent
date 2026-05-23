using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class UpdateSavingProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<UpdateSavingProductCommand, Result<SavingProductDetailDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<SavingProductDetailDto>> Handle( UpdateSavingProductCommand command, CancellationToken ct) {
            var req = command.Request;
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"SAVING-PRODUCT-{command.Id}";

            var p = await _uow.Products.GetSavingProductByIdAsync(command.Id, ct);
            if (p is null) return Result<SavingProductDetailDto>.NotFound("Saving product not found.");

            p.ProductName = req.ProductName;
            p.Description = req.Description;
            p.ProductTypeId = req.ProductTypeId;
            p.ChargeGroupId = req.ChargeGroupId;
            p.VatInclusive = req.VatInclusive;
            p.UseChargeGroups = req.UseChargeGroups;
            p.LimitWithdraw = req.LimitWithdraw;
            p.MaximumWithdraws = req.MaximumWithdraws;
            p.WithdrawPenalty = req.WithdrawPenalty;
            p.ChargeWithdraws = req.ChargeWithdraws;
            p.AllowOverdraft = req.AllowOverdraft;
            p.OverdraftInterest = req.OverdraftInterest;
            p.MinimumBalance = req.MinimumBalance;
            p.OfferInterest = req.OfferInterest;
            p.InterestRate = req.InterestRate;
            p.MinimumInterestOffered = req.MinimumInterestOffered;
            p.UpdatedBy = _userContext.Username;

            _uow.Products.UpdateSavingProduct(p);
            await _uow.CommitAsync(ct);

            var updated = await _uow.Products.GetSavingProductByIdAsync(p.Id, ct);
            return Result<SavingProductDetailDto>.Success(ProductMapper.MapSavingDetail(updated!));
        }
    }
}
