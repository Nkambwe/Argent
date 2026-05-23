using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class UpdateShareProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<UpdateShareProductCommand, Result<ShareProductDetailDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<ShareProductDetailDto>> Handle(UpdateShareProductCommand command, CancellationToken ct) {
            var p = await _uow.Products.GetShareProductByIdAsync(command.Id, ct);
            if (p is null) return Result<ShareProductDetailDto>.NotFound("Share product not found.");
            var req = command.Request;
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"SHARE-PRODUCTS-{command.Id}";

            p.ProductName = req.ProductName; 
            p.Description = req.Description;
            p.ProductTypeId = req.ProductTypeId; 
            p.ChargeGroupId = req.ChargeGroupId;
            p.VatInclusive = req.VatInclusive;
            p.UseChargeGroups = req.UseChargeGroups;
            p.UpdatedBy = _userContext.Username;
            _uow.Products.UpdateShareProduct(p);
            await _uow.CommitAsync(ct);
            var updated = await _uow.Products.GetShareProductByIdAsync(p.Id, ct);

            logger.Log($"Share Product updated: {p.Code} — {p.ProductName}", "PDT-OK");

            return Result<ShareProductDetailDto>.Success(ProductMapper.MapShareDetail(updated!));
        }
    }

}
