using Argent.Api.Domain.Entities;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organizations {

    /// <summary>
    /// Command handler for create new organization
    /// </summary>
    public class CreateOrganizationCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<CreateOrganizationCommand, Result<OrganizationDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<OrganizationDto>> Handle(
            CreateOrganizationCommand command, CancellationToken ct) {
            // Only one organization is allowed per deployment
            var existing = await _uow.Organizations.GetOrganizationAsync(ct);
            if (existing is not null)
                return Result<OrganizationDto>.Failure(
                    "An organization already exists. Use update to modify it.", "ALREADY_EXISTS");

            var req = command.Request;
            var org = new Organization
            {
                RegisteredName = req.RegisteredName,
                ShortName = req.ShortName,
                RegistrationNumber = req.RegistrationNumber,
                BusinessLine = req.BusinessLine,
                ContactEmail = req.ContactEmail,
                IsActive = true,
                CreatedBy = _userContext.Username
            };

            await _uow.Organizations.AddAsync(org, ct);
            await _uow.CommitAsync(ct);

            return Result<OrganizationDto>.Success(new OrganizationDto
            {
                Id = org.Id,
                RegisteredName = org.RegisteredName,
                ShortName = org.ShortName,
                RegistrationNumber = org.RegistrationNumber,
                BusinessLine = org.BusinessLine,
                ContactEmail = org.ContactEmail,
                IsActive = org.IsActive,
                BranchCount = 0,
                CreatedOn = org.CreatedOn
            });
        }
    }

}
