using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class DeactivateLoanProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<DeactivateLoanProductCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result> Handle(DeactivateLoanProductCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"LOAN-PRODUCT-{command.Id}-DEACTIVATE";

            var p = await _uow.Products.GetLoanProductByIdAsync(command.Id, ct);
            if (p is null) return Result.Failure("Loan product not found.", "NOT_FOUND");
            if (!p.IsActive) return Result.Failure("Product is already inactive.", "ALREADY_INACTIVE");
            p.IsActive = false; p.UpdatedBy = _userContext.Username;
            _uow.Products.UpdateLoanProduct(p); await _uow.CommitAsync(ct);

            logger.Log($"Loan Product deactvated: {p.Code} — {p.ProductName}", "PDT-OK");
            return Result.Success();
        }
    }
}
