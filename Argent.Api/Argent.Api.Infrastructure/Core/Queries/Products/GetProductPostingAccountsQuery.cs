using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Products {
    public record GetProductPostingAccountsQuery(long ProductId, ProductModuleType Module) : IRequest<Result<IEnumerable<PostingAccountDto>>>;
}
