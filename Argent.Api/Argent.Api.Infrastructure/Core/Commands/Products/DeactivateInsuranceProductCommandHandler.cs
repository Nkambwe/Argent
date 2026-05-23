using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class DeactivateInsuranceProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<DeactivateInsuranceProductCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result> Handle(DeactivateInsuranceProductCommand cmd, CancellationToken ct) {
            var p = await _uow.Products.GetInsuranceProductByIdAsync(cmd.Id, ct);
            if (p is null) 
                return Result.Failure("Insurance product not found.", "NOT_FOUND");

            if (!p.IsActive) 
                return Result.Failure("Product is already inactive.", "ALREADY_INACTIVE");

            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"INSURANCE-PRODUCT-{p.Code}";

            p.IsActive = false; p.UpdatedBy = _userContext.Username;
            _uow.Products.UpdateInsuranceProduct(p); await _uow.CommitAsync(ct);

            logger.Log($"Insurance Product deactivated: {p.Code} — {p.ProductName}", "PDT-OK");

            return Result.Success();
        }
    }

}
