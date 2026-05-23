using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class DeleteShareProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<DeleteShareProductCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result> Handle(DeleteShareProductCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"SHARE-PRODUCTS-{command.Id}";

            var p = await _uow.Products.GetShareProductByIdAsync(command.Id, ct);
            if (p is null) 
                return Result.Failure("Share product not found.", "NOT_FOUND");

            if (p.IsActive) 
                return Result.Failure("Deactivate the product before deleting it.", "PRODUCT_ACTIVE");

            p.IsDeleted = true; p.DeletedOn = DateTime.UtcNow; 
            p.DeletedBy = _userContext.Username;

            logger.Log($"Share product {p.Code} deleted.", "PDT-OK");
            _uow.Products.UpdateShareProduct(p); await _uow.CommitAsync(ct); return Result.Success();
        }
    }

}
