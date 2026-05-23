using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class UpdateProductTypeCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory) 
        : IRequestHandler<UpdateProductTypeCommand, Result<ProductTypeDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<ProductTypeDto>> Handle(UpdateProductTypeCommand command, CancellationToken ct) {
            var req = command.Request;
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"MANAGE-PRODUCTS{req.Name.ToUpper()}";

            var type = await _uow.Products.GetProductTypeByIdAsync(command.Id, ct);
            if (type is null) return Result<ProductTypeDto>.NotFound("Product type not found.");
            if (type.IsSystem)
                return Result<ProductTypeDto>.Failure(
                    "System product types cannot be modified.", "SYSTEM_TYPE");

            return await _uow.ExecuteInTransactionAsync(async token => {
                var req = command.Request;
                type.Name = req.Name;
                type.Series = req.Series;
                type.IsActive = req.IsActive;
                type.Description = req.Description;
                type.UpdatedBy = _userContext.Username;

                _uow.Products.UpdateProductType(type);
                logger.Log($"Product Type Updated: {type.Code} — {type.Name}", "PDT-OK");

                return Result<ProductTypeDto>.Success(new ProductTypeDto
                {
                    Id = type.Id, Code = type.Code,
                    Name = type.Name,
                    Module = type.Module.ToString(), 
                    Series = type.Series,
                    IsActive = type.IsActive, 
                    IsSystem = type.IsSystem,
                    Description = type.Description
                });
            }, ct);
            
        }
    }
}
