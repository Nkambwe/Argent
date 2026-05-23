using Argent.Api.Domain.Entities.Products;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class AddTimedepositRateCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<AddTimedepositRateCommand, Result<TimedepositRateDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<TimedepositRateDto>> Handle(AddTimedepositRateCommand command, CancellationToken ct) {
            var p = await _uow.Products.GetTimedepositProductByIdAsync(command.ProductId, ct);
            if (p is null)
                return Result<TimedepositRateDto>.NotFound("Time deposit product not found.");
            if (p.TierInterest)
                return Result<TimedepositRateDto>.Failure(
                    "This product uses tier-based interest. Add interest tiers instead.",
                    "USE_TIERS");

            var req = command.Request;
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"TIMEDEPOSIT-PRODUCT-{p.Code}";

            var rate = new TimedepositRate
            {
                TimedepositProductId = command.ProductId,
                Period = req.Period,
                PeriodType = req.PeriodType,
                InterestRate = req.InterestRate,
                MinimumAmount = req.MinimumAmount,
                IsActive = true,
                CreatedBy = _userContext.Username
            };
            await _uow.Products.AddTimedepositRateAsync(rate, ct);
            await _uow.CommitAsync(ct);

            logger.Log($"Timedeposit rate added for product: {p.Code} — {p.ProductName}", "PDT-OK");

            return Result<TimedepositRateDto>.Success(new TimedepositRateDto
            {
                Id = rate.Id,
                Period = rate.Period,
                PeriodType = rate.PeriodType.ToString(),
                InterestRate = rate.InterestRate,
                MinimumAmount = rate.MinimumAmount,
                IsActive = rate.IsActive
            });
        }
    }
}
