using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class UpdateInsuranceProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<UpdateInsuranceProductCommand, Result<InsuranceProductDetailDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<InsuranceProductDetailDto>> Handle(UpdateInsuranceProductCommand command, CancellationToken ct) {
            var p = await _uow.Products.GetInsuranceProductByIdAsync(command.Id, ct);
            if (p is null)
                return Result<InsuranceProductDetailDto>.NotFound("Insurance product not found.");

            var req = command.Request;
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"INSURANCE-PRODUCT-{p.Code}";

            p.ProductName = req.ProductName;
            p.Description = req.Description;
            p.ProductTypeId = req.ProductTypeId;
            p.ChargeGroupId = req.ChargeGroupId;
            p.VatInclusive = req.VatInclusive;
            p.UseChargeGroups = req.UseChargeGroups;
            p.UpdatedBy = _userContext.Username;

            _uow.Products.UpdateInsuranceProduct(p);
            await _uow.CommitAsync(ct);

            logger.Log($"Insurance Product updated: {p.Code} — {p.ProductName}", "PDT-OK");

            var updated = await _uow.Products.GetInsuranceProductByIdAsync(p.Id, ct);
            return Result<InsuranceProductDetailDto>.Success(ProductMapper.MapInsuranceDetail(updated!));
        }
    }

}
