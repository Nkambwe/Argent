using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class DeactivateSavingProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<DeactivateSavingProductCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;
        public async Task<Result> Handle(DeactivateSavingProductCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"SAVING-PRODUCT-{command.Id}-DEACTIVATE";

            var p = await _uow.Products.GetSavingProductByIdAsync(command.Id, ct);
            if (p is null) return Result.Failure("Saving product not found.", "NOT_FOUND");
            if (!p.IsActive) return Result.Failure("Product is already inactive.", "ALREADY_INACTIVE");
            p.IsActive = false; p.UpdatedBy = _userContext.Username;
            _uow.Products.UpdateSavingProduct(p);
            await _uow.CommitAsync(ct);

            logger.Log($"Saving Product {p.Code} deactivated", "PDT-OK");
            return Result.Success();
        }
    }
}
