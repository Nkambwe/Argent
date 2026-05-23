using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class UpdateTimedepositProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<UpdateTimedepositProductCommand, Result<TimedepositProductDetailDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<TimedepositProductDetailDto>> Handle(UpdateTimedepositProductCommand command, CancellationToken ct) {
            var p = await _uow.Products.GetTimedepositProductByIdAsync(command.Id, ct);
            if (p is null)
                return Result<TimedepositProductDetailDto>.NotFound("Time deposit product not found.");

            var req = command.Request;
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"TIMEDEPOSIT-PRODUCT-{p.Code}";


            p.ProductName = req.ProductName;
            p.Description = req.Description;
            p.ProductTypeId = req.ProductTypeId;
            p.ChargeGroupId = req.ChargeGroupId;
            p.VatInclusive = req.VatInclusive;
            p.UseChargeGroups = req.UseChargeGroups;
            p.WithdrawMode = req.WithdrawMode;
            p.CapitalizeInterest = req.CapitalizeInterest;
            p.ForfeitInterestForPrematureWithdraw = req.ForfeitInterestForPrematureWithdraw;
            p.PrematureWithdrawPenalty = req.PrematureWithdrawPenalty;
            p.Period = req.Period;
            p.PeriodType = req.PeriodType;
            p.MinimumAmount = req.MinimumAmount;
            p.MaximumAmount = req.MaximumAmount;
            p.TierInterest = req.TierInterest;
            p.TierMethod = req.TierMethod;
            p.UpdatedBy = _userContext.Username;

            _uow.Products.UpdateTimedepositProduct(p);
            await _uow.CommitAsync(ct);

            logger.Log($"Timedeposit Product updated: {p.Code} — {p.ProductName}", "PDT-OK");

            var updated = await _uow.Products.GetTimedepositProductByIdAsync(p.Id, ct);
            return Result<TimedepositProductDetailDto>.Success(
                ProductMapper.MapTimedepositDetail(updated!));
        }
    }
}
