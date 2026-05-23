using Argent.Api.Domain.Entities.Products;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class CreateProductTypeCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<CreateProductTypeCommand, Result<ProductTypeDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<ProductTypeDto>> Handle(CreateProductTypeCommand command, CancellationToken ct) {
            var req = command.Request;
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"MANAGE-PRODUCTS{req.Name.ToUpper()}";

            if (await _uow.Products.ProductTypeCodeExistsAsync(req.Code, ct: ct)) {
                return Result<ProductTypeDto>.Failure($"Product type code '{req.Code}' already exists.", "DUPLICATE_CODE");
            }

            return await _uow.ExecuteInTransactionAsync(async token => {
                var type = new ProductType
                {
                    Code = req.Code,
                    Name = req.Name,
                    Module = req.Module,
                    Series = req.Series,
                    IsSystem = false,
                    IsActive = true,
                    Description = req.Description,
                    CreatedBy = _userContext.Username
                };
                await _uow.Products.AddProductTypeAsync(type, ct);
                logger.Log($"Product Type Added: {type.Code} — {type.Name}", "PDT-OK");

                return Result<ProductTypeDto>.Success(new ProductTypeDto {
                    Id = type.Id, 
                    Code = type.Code,
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
