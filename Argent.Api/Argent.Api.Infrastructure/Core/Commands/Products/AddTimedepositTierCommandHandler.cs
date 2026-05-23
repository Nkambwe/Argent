using Argent.Api.Domain.Entities.Products;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class AddTimedepositTierCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<AddTimedepositTierCommand, Result<TimedepositTierDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<TimedepositTierDto>> Handle(AddTimedepositTierCommand command, CancellationToken ct) {
            var p = await _uow.Products.GetTimedepositProductByIdAsync(command.ProductId, ct);
            if (p is null)
                return Result<TimedepositTierDto>.NotFound("Time deposit product not found.");
            if (!p.TierInterest)
                return Result<TimedepositTierDto>.Failure(
                    "This product does not use tier-based interest. " +
                    "Enable TierInterest on the product first.", "NOT_TIER");

            var req = command.Request;
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"TIMEDEPOSIT-PRODUCT-TIER-{p.Code}";

            var tier = new TimedepositInterestTier
            {
                TimedepositProductId = command.ProductId,
                FromAmount = req.FromAmount,
                ToAmount = req.ToAmount,
                Rate = req.Rate,
                IsActive = true,
                CreatedBy = _userContext.Username
            };
            await _uow.Products.AddTimedepositTierAsync(tier, ct);
            await _uow.CommitAsync(ct);

            logger.Log($"Timedeposit tier added for product: {p.Code} — {p.ProductName}-{req.Rate}", "PDT-OK");

            return Result<TimedepositTierDto>.Success(new TimedepositTierDto
            {
                Id = tier.Id,
                FromAmount = tier.FromAmount,
                ToAmount = tier.ToAmount,
                Rate = tier.Rate,
                IsActive = tier.IsActive
            });
        }
    }
}
