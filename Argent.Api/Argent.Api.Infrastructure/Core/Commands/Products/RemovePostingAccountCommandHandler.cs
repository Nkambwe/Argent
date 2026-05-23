using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class RemovePostingAccountCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<RemovePostingAccountCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result> Handle(RemovePostingAccountCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"POSTING-PROUCT-ACCOUNT-{command.ProductId}";

            var account = await _uow.Products.GetPostingAccountAsync(command.ProductId, command.Module, command.Purpose, command.Segment, ct);
            if (account is null) return Result.Failure("Posting account mapping not found.", "NOT_FOUND");
            _uow.Products.RemovePostingAccount(account);
            await _uow.CommitAsync(ct);

            logger.Log($"Posting account for product {command.ProductId} removed.", "PDT-OK");

            return Result.Success();
        }
    }

}
