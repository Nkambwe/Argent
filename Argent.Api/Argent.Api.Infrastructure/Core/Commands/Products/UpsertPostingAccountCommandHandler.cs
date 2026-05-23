using Argent.Api.Domain.Entities.Products;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class UpsertPostingAccountCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<UpsertPostingAccountCommand, Result<PostingAccountDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<PostingAccountDto>> Handle(UpsertPostingAccountCommand command, CancellationToken ct) {
            var req = command.Request;
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"POSTING-PROUCT-ACCOUNT-{command.ProductId}";

            var existing = await _uow.Products.GetPostingAccountAsync(command.ProductId, command.Module, req.PostingPurpose, req.CustomerSegment, ct);

            if (existing is not null) {
                existing.LedgerNumber = req.LedgerNumber;
                existing.CostCentreCode = req.CostCentreCode;
                existing.RevenueCentreCode = req.RevenueCentreCode;
                existing.UpdatedBy = _userContext.Username;
                _uow.Products.UpdatePostingAccount(existing);
                await _uow.CommitAsync(ct);

                logger.Log($"Posting account for product {command.ProductId} updated.", "PDT-OK");
                return Result<PostingAccountDto>.Success(MapPostingAccount(existing));
            }

            var account = new ProductPostingAccount
            {
                ProductId = command.ProductId,
                ProductModule = command.Module,
                PostingPurpose = req.PostingPurpose,
                CustomerSegment = req.CustomerSegment,
                LedgerNumber = req.LedgerNumber,
                CostCentreCode = req.CostCentreCode,
                RevenueCentreCode = req.RevenueCentreCode,
                CreatedBy = _userContext.Username
            };
            await _uow.Products.AddPostingAccountAsync(account, ct);
            await _uow.CommitAsync(ct);
            logger.Log($"Posting account for product {command.ProductId} added.", "PDT-OK");
            return Result<PostingAccountDto>.Success(MapPostingAccount(account));
        }

        private static PostingAccountDto MapPostingAccount(ProductPostingAccount a) => new()
        {
            Id = a.Id, ProductId = a.ProductId, ProductModule = a.ProductModule.ToString(),
            PostingPurpose = a.PostingPurpose.ToString(), CustomerSegment = a.CustomerSegment.ToString(),
            LedgerNumber = a.LedgerNumber, CostCentreCode = a.CostCentreCode,
            RevenueCentreCode = a.RevenueCentreCode
        };
    }

}
