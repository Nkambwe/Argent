using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organization {
    /// <summary>
    /// Command Handler for updating organization
    /// </summary>
    /// <param name="uow"></param>
    public class UpdateOrganizationCommandHandler(IUnitOfWork uow)
                : IRequestHandler<UpdateOrganizationCommand,
        Result<OrganizationDto>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<OrganizationDto>> Handle(UpdateOrganizationCommand command, CancellationToken token) {
            var org = await _uow.Organizations.GetWithBranchesAsync(command.OrganizationId, token);
            if (org is null)
                return Result<OrganizationDto>.NotFound("Organization not found.");

            org.RegisteredName = command.RegisteredName;
            org.ShortName = command.ShortName;
            org.BusinessLine = command.BusinessLine;
            org.ContactEmail = command.ContactEmail;

            _uow.Organizations.Update(org);
            await _uow.CommitAsync(token);

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
                Branches = [.. org.Branches.Select(b => new BranchDto
                {
                    Id = b.Id,
                    OrganizationId = b.OrganizationId,
                    BranchName = b.BranchName,
                    Address = b.Address,
                    EmailAddress = b.EmailAddress,
                    PostalAddress = b.PostalAddress,
                    IsDefault = b.IsDefault,
                    IsActive = b.IsActive,
                    CreatedOn = b.CreatedOn
                })]
            });
        }
    }
}
