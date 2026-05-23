using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Products {
    public class GetProductPostingAccountsQueryHandler(IUnitOfWork uow) : IRequestHandler<GetProductPostingAccountsQuery, Result<IEnumerable<PostingAccountDto>>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<IEnumerable<PostingAccountDto>>> Handle(GetProductPostingAccountsQuery query, CancellationToken ct) {
            var accounts = await _uow.Products.GetPostingAccountsAsync(
                query.ProductId, query.Module, ct);
            return Result<IEnumerable<PostingAccountDto>>.Success(accounts.Select(a => ProductMapper.MapPostingAccount(a)));
        }
    }
}
