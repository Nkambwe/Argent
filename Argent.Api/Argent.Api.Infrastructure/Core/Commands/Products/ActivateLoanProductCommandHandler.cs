using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class ActivateLoanProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<ActivateLoanProductCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result> Handle(ActivateLoanProductCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"LOAN-PRODUCT-{command.Id}-ACTIVATE";

            var p = await _uow.Products.GetLoanProductByIdAsync(command.Id, ct);
            if (p is null) return Result.Failure("Loan product not found.", "NOT_FOUND");
            if (p.IsActive) return Result.Failure("Product is already active.", "ALREADY_ACTIVE");
            p.IsActive = true; p.UpdatedBy = _userContext.Username;
            _uow.Products.UpdateLoanProduct(p); await _uow.CommitAsync(ct);

            logger.Log($"Loan Product activated: {p.Code} — {p.ProductName}", "PDT-OK");
            return Result.Success();
        }
    }
}
