using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class DeactivateShareProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<DeactivateShareProductCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result> Handle(DeactivateShareProductCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"SHARE-PRODUCTS-{command.Id}";

            var p = await _uow.Products.GetShareProductByIdAsync(command.Id, ct);
            if (p is null) 
                return Result.Failure("Share product not found.", "NOT_FOUND");

            if (!p.IsActive) 
                return Result.Failure("Product is already inactive.", "ALREADY_INACTIVE");

            p.IsActive = false;
            p.UpdatedBy = _userContext.Username;
            _uow.Products.UpdateShareProduct(p);
            await _uow.CommitAsync(ct); 

            logger.Log($"Share Product {p.Code} deactivated", "PDT-OK");
            return Result.Success();
        }
    }

}
