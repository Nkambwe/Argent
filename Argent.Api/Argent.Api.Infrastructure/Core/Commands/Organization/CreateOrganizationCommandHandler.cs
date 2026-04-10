using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organization {
    /// <summary>
    /// Command handler for create new organization
    /// </summary>
    public class CreateOrganizationCommandHandler(IUnitOfWork uow)
        : IRequestHandler<CreateOrganizationCommand, Result<OrganizationDto>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<OrganizationDto>> Handle(
            CreateOrganizationCommand command,
            CancellationToken ct) {
            //check if organization exists, only one organization allowed in this deployment
            var existing = await _uow.Organizations.GetAllAsync(ct);
            if (existing.Any())
                return Result<OrganizationDto>.Failure(
                    "An organization already exists. Use the update endpoint to modify it.",
                    "ORGANIZATION_EXISTS");

            //..check duplicate registration number
            if (await _uow.Organizations.RegistrationNumberExistsAsync(command.RegistrationNumber, ct))
                return Result<OrganizationDto>.Failure(
                    $"Registration number '{command.RegistrationNumber}' is already in use.",
                    "DUPLICATE_REGISTRATION_NUMBER");

            await _uow.BeginTransactionAsync(ct);
            try {
                //..create organization
                var organization = new Domain.Entities.Organization
                {
                    RegisteredName = command.RegisteredName,
                    ShortName = command.ShortName,
                    RegistrationNumber = command.RegistrationNumber,
                    BusinessLine = command.BusinessLine,
                    ContactEmail = command.ContactEmail,
                    IsActive = true
                };
                await _uow.Organizations.AddAsync(organization, ct);

                //..create the mandatory default branch in the same transaction
                var defaultBranch = new Domain.Entities.Branch {
                    OrganizationId = organization.Id,
                    BranchName = command.DefaultBranchName,
                    Address = command.DefaultBranchAddress,
                    EmailAddress = command.DefaultBranchEmail,
                    PostalAddress = command.DefaultBranchPostalAddress,
                    IsDefault = true,
                    IsActive = true
                };
                organization.Branches.Add(defaultBranch);

                await _uow.CommitAsync(ct);

                return Result<OrganizationDto>.Success(MapToDto(organization));
            }
            catch {
                await _uow.RollbackAsync(ct);
                throw;
            }
        }

        private static OrganizationDto MapToDto(Domain.Entities.Organization org) => new()
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
        };
    }

}
