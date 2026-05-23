using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class ActivateInsuranceProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<ActivateInsuranceProductCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result> Handle(ActivateInsuranceProductCommand cmd, CancellationToken ct) {

            var p = await _uow.Products.GetInsuranceProductByIdAsync(cmd.Id, ct);
            if (p is null) 
                return Result.Failure("Insurance product not found.", "NOT_FOUND");

            if (p.IsActive) 
                return Result.Failure("Product is already active.", "ALREADY_ACTIVE");

            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"INSURANCE-PRODUCT-{p.Code}";

            p.IsActive = true; p.UpdatedBy = _userContext.Username;
            _uow.Products.UpdateInsuranceProduct(p); await _uow.CommitAsync(ct);

            logger.Log($"Insurance Product activated: {p.Code} — {p.ProductName}", "PDT-OK");

            return Result.Success();
        }
    }

}
