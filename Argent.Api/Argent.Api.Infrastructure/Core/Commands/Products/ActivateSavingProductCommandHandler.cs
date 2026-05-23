using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class ActivateSavingProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<ActivateSavingProductCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result> Handle(ActivateSavingProductCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"SAVING-PRODUCT-{command.Id}-ACTIVATE";

            var p = await _uow.Products.GetSavingProductByIdAsync(command.Id, ct);
            if (p is null) return Result.Failure("Saving product not found.", "NOT_FOUND");
            if (p.IsActive) return Result.Failure("Product is already active.", "ALREADY_ACTIVE");
            p.IsActive = true; p.UpdatedBy = _userContext.Username;
            _uow.Products.UpdateSavingProduct(p);
            await _uow.CommitAsync(ct);

            logger.Log($"Saving Product {p.Code} activated", "PDT-OK");
            return Result.Success();
        }
    }
}
