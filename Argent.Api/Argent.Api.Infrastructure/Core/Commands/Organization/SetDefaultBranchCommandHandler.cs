using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organization {
    /// <summary>
    /// Command handler for setting default branch
    /// </summary>
    /// <param name="uow"></param>
    public class SetDefaultBranchCommandHandler(IUnitOfWork uow) : 
        IRequestHandler<SetDefaultBranchCommand, 
            Result<BranchDto>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<BranchDto>> Handle(
            SetDefaultBranchCommand command,
            CancellationToken ct) {

            //..confirm branch belongs to this organization
            var branch = await _uow.Organizations.GetBranchByIdAsync(command.BranchId, ct);
            if (branch is null || branch.OrganizationId != command.OrganizationId)
                return Result<BranchDto>.NotFound("Branch not found for this organization.");

            if (branch.IsDefault)
                return Result<BranchDto>.Failure("This branch is already the default.", "ALREADY_DEFAULT");

            await _uow.BeginTransactionAsync(ct);
            try {
                await _uow.Organizations.ClearAndSetDefaultBranchAsync(
                    command.OrganizationId,
                    command.BranchId,
                    ct);

                await _uow.CommitAsync(ct);

                //..refresh branch state after commit
                var updated = await _uow.Organizations.GetBranchByIdAsync(command.BranchId, ct);

                return Result<BranchDto>.Success(new BranchDto
                {
                    Id = updated!.Id,
                    OrganizationId = updated.OrganizationId,
                    BranchCode = updated.BranchCode,
                    BranchName = updated.BranchName,
                    Address = updated.Address,
                    EmailAddress = updated.EmailAddress,
                    PostalAddress = updated.PostalAddress,
                    IsDefault = updated.IsDefault,
                    IsActive = updated.IsActive,
                    CreatedOn = updated.CreatedOn
                });
            } catch {
                await _uow.RollbackAsync(ct);
                throw;
            }
        }
    }
}
