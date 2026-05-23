using Argent.Api.Domain.Entities.Products;
using Argent.Api.Domain.Entities.Settings;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class CreateShareProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<CreateShareProductCommand, Result<ShareProductDetailDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<ShareProductDetailDto>> Handle(CreateShareProductCommand command, CancellationToken ct) {
            var req = command.Request;
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"SHARE-PRODUCTS-{req.Code.ToUpper()}";

            if (await _uow.Products.ProductCodeExistsAsync(req.Code, ProductModuleType.Share, ct: ct))
                return Result<ShareProductDetailDto>.Failure(
                    $"Product code '{req.Code}' already exists.", "DUPLICATE_CODE");

            var productType = await _uow.Products.GetProductTypeByIdAsync(req.ProductTypeId, ct);
            if (productType?.Module != ProductModuleType.Share)
                return Result<ShareProductDetailDto>.Failure(
                    "Product type does not belong to the Share module.", "WRONG_MODULE");

            return await _uow.ExecuteInTransactionAsync(async token =>
            {
                var product = new ShareProduct
                {
                    Code = req.Code, 
                    ProductName = req.ProductName, 
                    Description = req.Description,
                    ProductTypeId = req.ProductTypeId, 
                    ChargeGroupId = req.ChargeGroupId,
                    VatInclusive = req.VatInclusive, 
                    UseChargeGroups = req.UseChargeGroups,
                    IsActive = true, 
                    CreatedBy = _userContext.Username
                };
                await _uow.Products.AddShareProductAsync(product, token);
                await _uow.CommitAsync(token);

                logger.Log($"Share Product Registered: {product.Code} — {product.ProductName}", "PDT-OK");

                product.Configuration = new ShareProductConfiguration
                {
                    ShareProductId = product.Id, 
                    DividendCalculationMethod = DividendCalculationMethod.None,
                    DividendCalculationInterval = IntervalType.Months, 
                    AllowShareRedemption = true,
                    CreatedBy = _userContext.Username
                };
                _uow.Products.UpdateShareProduct(product);

                foreach (var pa in req.PostingAccounts)
                    await _uow.Products.AddPostingAccountAsync(new ProductPostingAccount
                    {
                        ProductId = product.Id, 
                        ProductModule = ProductModuleType.Share,
                        PostingPurpose = pa.PostingPurpose, 
                        CustomerSegment = pa.CustomerSegment,
                        LedgerNumber = pa.LedgerNumber, 
                        CostCentreCode = pa.CostCentreCode,
                        RevenueCentreCode = pa.RevenueCentreCode,
                        CreatedBy = _userContext.Username
                    }, token);

                var created = await _uow.Products.GetShareProductByIdAsync(product.Id, token);
                return Result<ShareProductDetailDto>.Success(ProductMapper.MapShareDetail(created!));
            }, ct);
        }
    }

}
