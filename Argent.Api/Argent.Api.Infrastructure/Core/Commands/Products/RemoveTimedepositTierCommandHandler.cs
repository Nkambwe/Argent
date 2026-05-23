using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class RemoveTimedepositTierCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<RemoveTimedepositTierCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result> Handle(RemoveTimedepositTierCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"TIMEDEPOSIT-TIER-{command.TierId}";

            var tier = await _uow.Products.GetTimedepositTierByIdAsync(command.TierId, ct);
            if (tier is null) return Result.Failure("Interest tier not found.", "NOT_FOUND");
            _uow.Products.RemoveTimedepositTier(tier);
            await _uow.CommitAsync(ct);

            logger.Log($"Timedeposit tier removed: {tier.Id} — {tier.Rate}", "PDT-OK");

            return Result.Success();
        }
    }
}
