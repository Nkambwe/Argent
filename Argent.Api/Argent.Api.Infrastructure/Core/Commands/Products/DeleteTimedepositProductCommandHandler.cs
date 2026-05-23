using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class DeleteTimedepositProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<DeleteTimedepositProductCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result> Handle(DeleteTimedepositProductCommand cmd, CancellationToken ct) {
            var p = await _uow.Products.GetTimedepositProductByIdAsync(cmd.Id, ct);
            if (p is null) 
                return Result.Failure("Time deposit product not found.", "NOT_FOUND");

            if (p.IsActive) 
                return Result.Failure("Deactivate the product before deleting it.", "PRODUCT_ACTIVE");

            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"TIMEDEPOSIT-PRODUCT-{p.Code}";

            p.IsDeleted = true; 
            p.DeletedOn = DateTime.UtcNow;
            p.DeletedBy = _userContext.Username;
            _uow.Products.UpdateTimedepositProduct(p); await _uow.CommitAsync(ct);

            logger.Log($"Timedeposit Product deleted: {p.Code} — {p.ProductName}", "PDT-OK");

            return Result.Success();
        }
    }
}
