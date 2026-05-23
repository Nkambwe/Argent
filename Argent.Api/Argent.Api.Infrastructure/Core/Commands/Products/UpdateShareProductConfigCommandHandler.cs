using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class UpdateShareProductConfigCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<UpdateShareProductConfigCommand, Result<ShareProductDetailDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<ShareProductDetailDto>> Handle(UpdateShareProductConfigCommand command, CancellationToken ct) {
            var p = await _uow.Products.GetShareProductByIdAsync(command.Id, ct);
            if (p is null) return Result<ShareProductDetailDto>.NotFound("Share product not found.");
            if (p.Configuration is null) return Result<ShareProductDetailDto>.Failure("No configuration found.", "NO_CONFIG");
            
            var req = command.Request; 
            var cfg = p.Configuration;
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"SHARE-PRODUCTS-{p.Code.ToUpper()}";

            cfg.NominalValue = req.NominalValue;
            cfg.MinimumShareCapital = req.MinimumShareCapital;
            cfg.DividendCalculationMethod = req.DividendCalculationMethod;
            cfg.DividendCalculationPeriod = req.DividendCalculationPeriod;
            cfg.DividendCalculationInterval = req.DividendCalculationInterval;
            cfg.DividendRate = req.DividendRate; 
            cfg.DividendEarningShares = req.DividendEarningShares;
            cfg.ChargeWithholdingTaxOnDividends = req.ChargeWithholdingTaxOnDividends;
            cfg.AllowShareRedemption = req.AllowShareRedemption;
            cfg.MinimumSharesAfterRedemption = req.MinimumSharesAfterRedemption;
            cfg.RequireApprovalForRedemption = req.RequireApprovalForRedemption;
            cfg.UpdatedBy = _userContext.Username;
            _uow.Products.UpdateShareProduct(p); await _uow.CommitAsync(ct);
            var updated = await _uow.Products.GetShareProductByIdAsync(p.Id, ct);

            logger.Log($"Share Product Updated: {p.Code} — {p.ProductName}", "PDT-OK");

            return Result<ShareProductDetailDto>.Success(ProductMapper.MapShareDetail(updated!));
        }
    }

}
