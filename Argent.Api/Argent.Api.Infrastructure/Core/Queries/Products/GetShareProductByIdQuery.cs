using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Products {
    public record GetShareProductByIdQuery(long Id) : IRequest<Result<ShareProductDetailDto>>;
}
