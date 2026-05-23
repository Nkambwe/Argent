using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class RemoveTimedepositRateCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<RemoveTimedepositRateCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result> Handle(RemoveTimedepositRateCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"TIMEDEPOSIT-RATE-{command.RateId}";

            var rate = await _uow.Products.GetTimedepositRateByIdAsync(command.RateId, ct);
            if (rate is null) return Result.Failure("Interest rate not found.", "NOT_FOUND");
            _uow.Products.RemoveTimedepositRate(rate);
            await _uow.CommitAsync(ct);

            logger.Log($"Timedeposit Rate removed: {rate.Id} — {rate.InterestRate}", "PDT-OK");

            return Result.Success();
        }
    }
}
