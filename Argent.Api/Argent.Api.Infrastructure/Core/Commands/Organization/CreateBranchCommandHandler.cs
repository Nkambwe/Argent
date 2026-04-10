using Argent.Api.Domain.Entities;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organization {

    /// <summary>
    /// Command handler for create new Branch
    /// </summary>
    public class CreateBranchCommandHandler(IUnitOfWork uow) :
        IRequestHandler<CreateBranchCommand, Result<BranchDto>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<BranchDto>> Handle(CreateBranchCommand command, CancellationToken token) {

            //..verify organization exists
            var org = await _uow.Organizations.GetByIdAsync(command.OrganizationId, token);
            if (org is null)
                return Result<BranchDto>.NotFound("Organization not found.");

            //..check for duplicate branch name within org
            if (await _uow.Organizations.BranchNameExistsAsync(command.OrganizationId, command.BranchName, token))
                return Result<BranchDto>.Failure($"A branch named '{command.BranchName}' already exists in this organization.", "DUPLICATE_BRANCH_NAME");

            await _uow.BeginTransactionAsync(token);
            try {
                var branch = new Branch
                {
                    OrganizationId = command.OrganizationId,
                    BranchName = command.BranchName,
                    Address = command.Address,
                    EmailAddress = command.EmailAddress,
                    PostalAddress = command.PostalAddress,
                    IsDefault = command.MakeDefault,
                    IsActive = true
                };

                //..set this as the default branch
                if (command.MakeDefault)
                    await _uow.Organizations.ClearAndSetDefaultBranchAsync(command.OrganizationId, 0, token);

                await _uow.Organizations.AddBranchAsync(branch, token);
                await _uow.CommitAsync(token);

                return Result<BranchDto>.Success(new BranchDto {
                    Id = branch.Id,
                    OrganizationId = branch.OrganizationId,
                    BranchName = branch.BranchName,
                    Address = branch.Address,
                    EmailAddress = branch.EmailAddress,
                    PostalAddress = branch.PostalAddress,
                    IsDefault = branch.IsDefault,
                    IsActive = branch.IsActive,
                    CreatedOn = branch.CreatedOn
                });
            } catch {
                await _uow.RollbackAsync(token);
                throw;
            }
        }
    }
}
