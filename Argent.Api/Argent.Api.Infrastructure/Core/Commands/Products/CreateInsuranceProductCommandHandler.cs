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
    public class CreateInsuranceProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<CreateInsuranceProductCommand, Result<InsuranceProductDetailDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<InsuranceProductDetailDto>> Handle(CreateInsuranceProductCommand command, CancellationToken ct) {
            var req = command.Request;
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"INSURANCE-PRODUCT-{req.Code}";

            if (await _uow.Products.ProductCodeExistsAsync(
                    req.Code, ProductModuleType.Insurance, ct: ct))
                return Result<InsuranceProductDetailDto>.Failure(
                    $"Product code '{req.Code}' already exists.", "DUPLICATE_CODE");

            var productType = await _uow.Products.GetProductTypeByIdAsync(req.ProductTypeId, ct);
            if (productType?.Module != ProductModuleType.Insurance)
                return Result<InsuranceProductDetailDto>.Failure(
                    "Product type does not belong to the Insurance module.", "WRONG_MODULE");

            return await _uow.ExecuteInTransactionAsync(async token =>
            {
                var product = new InsuranceProduct
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

                await _uow.Products.AddInsuranceProductAsync(product, token);
                await _uow.CommitAsync(token);

                logger.Log($"Insurance Product created: {product.Code} — {product.ProductName}", "PDT-OK");

                product.Configuration = new InsuranceProductConfiguration
                {
                    InsuranceProductId = product.Id,
                    PolicyPeriod = 12,      
                    CreatedBy = _userContext.Username
                };
                _uow.Products.UpdateInsuranceProduct(product);

                foreach (var pa in req.PostingAccounts)
                    await _uow.Products.AddPostingAccountAsync(new ProductPostingAccount
                    {
                        ProductId = product.Id,
                        ProductModule = ProductModuleType.Insurance,
                        PostingPurpose = pa.PostingPurpose,
                        CustomerSegment = pa.CustomerSegment,
                        LedgerNumber = pa.LedgerNumber,
                        CostCentreCode = pa.CostCentreCode,
                        RevenueCentreCode = pa.RevenueCentreCode,
                        CreatedBy = _userContext.Username
                    }, token);

                var created = await _uow.Products.GetInsuranceProductByIdAsync(product.Id, token);
                return Result<InsuranceProductDetailDto>.Success(
                    ProductMapper.MapInsuranceDetail(created!));
            }, ct);
        }
    }

}
