using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class DeleteProductTypeCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
                : IRequestHandler<DeleteProductTypeCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result> Handle(DeleteProductTypeCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"DELETE-PRODUCTS-{command.Id}";
            var type = await _uow.Products.GetProductTypeByIdAsync(command.Id, ct);
            if (type is null) return Result.Failure("Product type not found.", "NOT_FOUND");
            if (type.IsSystem)
                return Result.Failure("System product types cannot be deleted.", "SYSTEM_TYPE");

            return await _uow.ExecuteInTransactionAsync(async token => {
                type.DeletedBy = _userContext.Username;
                _uow.Products.RemoveProductType(type);
                logger.Log($"Product Type Deleted: {type.Code} — {type.Name}", "PDT-OK");
                return Result.Success();
            }, ct);
            
        }
    }
}
