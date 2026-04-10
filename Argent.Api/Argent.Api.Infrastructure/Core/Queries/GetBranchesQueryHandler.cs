using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Queries {
    /// <summary>
    /// Query handler to retrieve branches
    /// </summary>
    /// <param name="uow"></param>
    public class GetBranchesQueryHandler(IUnitOfWork uow)
        : IRequestHandler<GetBranchesQuery, Result<IEnumerable<BranchDto>>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<IEnumerable<BranchDto>>> Handle(
            GetBranchesQuery query, CancellationToken ct) {
            var org = await _uow.Organizations.GetByIdAsync(query.OrganizationId, ct);
            if (org is null)
                return Result<IEnumerable<BranchDto>>.NotFound("Organization not found.");

            var branches = await _uow.Organizations.GetBranchesByOrganizationAsync(query.OrganizationId, ct);

            var dtos = branches.Select(b => new BranchDto {
                Id = b.Id,
                OrganizationId = b.OrganizationId,
                BranchName = b.BranchName,
                Address = b.Address,
                EmailAddress = b.EmailAddress,
                PostalAddress = b.PostalAddress,
                IsDefault = b.IsDefault,
                IsActive = b.IsActive,
                CreatedOn = b.CreatedOn
            });

            return Result<IEnumerable<BranchDto>>.Success(dtos);
        }
    }
}
