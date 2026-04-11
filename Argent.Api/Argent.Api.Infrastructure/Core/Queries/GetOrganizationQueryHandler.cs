using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries {
    public class GetOrganizationQueryHandler(IUnitOfWork uow)
        : IRequestHandler<GetOrganizationQuery, Result<OrganizationDto>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<OrganizationDto>> Handle(GetOrganizationQuery query, CancellationToken token) {
            Domain.Entities.Organization? org;

            if (query.OrganizationId.HasValue) {
                org = await _uow.Organizations.GetWithBranchesAsync(query.OrganizationId.Value, token);
            }
            else {
                // Single-org deployment: return the first active organization
                var all = await _uow.Organizations.FindAsync(o => o.IsActive, token);
                var first = all.FirstOrDefault();
                org = first is null ? null : await _uow.Organizations.GetWithBranchesAsync(first.Id, token);
            }

            if (org is null)
                return Result<OrganizationDto>.NotFound("No organization found. Please complete the initial setup.");

            return Result<OrganizationDto>.Success(new OrganizationDto
            {
                Id = org.Id,
                RegisteredName = org.RegisteredName,
                ShortName = org.ShortName,
                RegistrationNumber = org.RegistrationNumber,
                BusinessLine = org.BusinessLine,
                ContactEmail = org.ContactEmail,
                IsActive = org.IsActive,
                CreatedAt = org.CreatedOn,
                Branches = org.Branches.Select(b => new BranchDto
                {
                    Id = b.Id,
                    OrganizationId = b.OrganizationId,
                    BranchCode = b.BranchCode,
                    BranchName = b.BranchName,
                    Address = b.Address,
                    EmailAddress = b.EmailAddress,
                    PostalAddress = b.PostalAddress,
                    IsDefault = b.IsDefault,
                    IsActive = b.IsActive,
                    CreatedOn = b.CreatedOn
                }).OrderByDescending(b => b.IsDefault).ThenBy(b => b.BranchCode)
            });
        }
    }

}
