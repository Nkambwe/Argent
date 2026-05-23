using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class DeleteLoanProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<DeleteLoanProductCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result> Handle(DeleteLoanProductCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"LOAN-PRODUCTS-{command.Id}-DELETE";

            var p = await _uow.Products.GetLoanProductByIdAsync(command.Id, ct);
            if (p is null) return Result.Failure("Loan product not found.", "NOT_FOUND");
            if (p.IsActive) return Result.Failure("Deactivate the product before deleting it.", "PRODUCT_ACTIVE");
            p.IsDeleted = true; 
            p.DeletedOn = DateTime.UtcNow; 
            p.DeletedBy = _userContext.Username;
            _uow.Products.UpdateLoanProduct(p); await _uow.CommitAsync(ct);

            logger.Log($"Loan Product Registered: {p.Code} — {p.ProductName}", "PDT-OK");
            return Result.Success();
        }
    }
}
