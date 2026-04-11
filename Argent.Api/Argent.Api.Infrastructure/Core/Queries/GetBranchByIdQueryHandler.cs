using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries {
    public class GetBranchByIdQueryHandler(IUnitOfWork uow)
        : IRequestHandler<GetBranchByIdQuery, Result<BranchDto>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<BranchDto>> Handle(GetBranchByIdQuery query, CancellationToken ct) {
            var branch = await _uow.Organizations.GetBranchByIdAsync(query.BranchId, ct);

            if (branch is null || branch.OrganizationId != query.OrganizationId)
                return Result<BranchDto>.NotFound("Branch not found for this organization.");

            return Result<BranchDto>.Success(new BranchDto
            {
                Id = branch.Id,
                OrganizationId = branch.OrganizationId,
                BranchCode = branch.BranchCode,
                BranchName = branch.BranchName,
                Address = branch.Address,
                EmailAddress = branch.EmailAddress,
                PostalAddress = branch.PostalAddress,
                IsDefault = branch.IsDefault,
                IsActive = branch.IsActive,
                CreatedOn = branch.CreatedOn
            });
        }
    }
}
