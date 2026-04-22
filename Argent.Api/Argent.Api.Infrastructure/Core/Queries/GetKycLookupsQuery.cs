using Argent.Api.Infrastructure.Core.Common;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries {
    // ── GetKycLookups query ────────────────────────────────────────────────────

    public record GetKycLookupsQuery : IRequest<Result<KycLookupsDto>>;

}
