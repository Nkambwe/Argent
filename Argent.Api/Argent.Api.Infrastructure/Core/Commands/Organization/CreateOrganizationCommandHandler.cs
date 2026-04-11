using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organization {

    /// <summary>
    /// Command handler for create new organization
    /// </summary>
    public class CreateOrganizationCommandHandler(IUnitOfWork uow) : IRequestHandler<CreateOrganizationCommand, Result<OrganizationDto>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<OrganizationDto>> Handle(CreateOrganizationCommand command, CancellationToken token) {
            var existing = await _uow.Organizations.GetAllAsync(token);
            if (existing.Any())
                return Result<OrganizationDto>.Failure("An organization already exists. Use the update endpoint to modify it.", "ORGANIZATION_EXISTS");

            if (await _uow.Organizations.RegistrationNumberExistsAsync(command.RegistrationNumber, token))
                return Result<OrganizationDto>.Failure($"Registration number '{command.RegistrationNumber}' is already in use.", "DUPLICATE_REGISTRATION_NUMBER");

            return await _uow.ExecuteInTransactionAsync(async token =>
            {
                var organization = new Domain.Entities.Organization
                {
                    RegisteredName = command.RegisteredName,
                    ShortName = command.ShortName,
                    RegistrationNumber = command.RegistrationNumber,
                    BusinessLine = command.BusinessLine,
                    ContactEmail = command.ContactEmail,
                    IsActive = true
                };

                await _uow.Organizations.AddAsync(organization, token);

                var defaultBranch = new Domain.Entities.Branch
                {
                    BranchCode = command.DefaultBranchCode,
                    BranchName = command.DefaultBranchName,
                    Address = command.DefaultBranchAddress,
                    EmailAddress = command.DefaultBranchEmail,
                    PostalAddress = command.DefaultBranchPostalAddress,
                    IsDefault = true,
                    IsActive = true
                };

                organization.Branches.Add(defaultBranch);
                return Result<OrganizationDto>.Success(MapToDto(organization));
            }, token);
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
                BranchCode = b.BranchCode,
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
