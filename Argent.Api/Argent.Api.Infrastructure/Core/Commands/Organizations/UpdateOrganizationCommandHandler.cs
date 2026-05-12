using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organizations {
    /// <summary>
    /// Command Handler for updating organization
    /// </summary>
    /// <param name="uow"></param>
    public class UpdateOrganizationCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<UpdateOrganizationCommand, Result<OrganizationDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<OrganizationDto>> Handle(
            UpdateOrganizationCommand command, CancellationToken ct) {
            var org = await _uow.Organizations.GetByIdAsync(command.OrganizationId, ct);
            if (org is null)
                return Result<OrganizationDto>.NotFound("Organization not found.");

            var req = command.Request;
            org.RegisteredName = req.RegisteredName;
            org.ShortName = req.ShortName;
            org.RegistrationNumber = req.RegistrationNumber;
            org.BusinessLine = req.BusinessLine;
            org.ContactEmail = req.ContactEmail;
            org.IsActive = req.IsActive;
            org.UpdatedBy = _userContext.Username;

            _uow.Organizations.Update(org);
            await _uow.CommitAsync(ct);

            var updated = await _uow.Organizations.GetOrganizationAsync(ct);
            return Result<OrganizationDto>.Success(new OrganizationDto
            {
                Id = updated!.Id,
                RegisteredName = updated.RegisteredName,
                ShortName = updated.ShortName,
                RegistrationNumber = updated.RegistrationNumber,
                BusinessLine = updated.BusinessLine,
                ContactEmail = updated.ContactEmail,
                IsActive = updated.IsActive,
                BranchCount = updated.Branches.Count(b => !b.IsDeleted),
                CreatedOn = updated.CreatedOn
            });
        }
    }
}
