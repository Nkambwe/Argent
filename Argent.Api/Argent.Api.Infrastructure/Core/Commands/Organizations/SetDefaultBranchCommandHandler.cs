using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organizations {
    /// <summary>
    /// Command handler for setting default branch
    /// </summary>
    /// <param name="uow"></param>
    public class SetDefaultBranchCommandHandler(IUnitOfWork uow) :
        IRequestHandler<SetDefaultBranchCommand, Result<BranchDto>>{
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<BranchDto>> Handle(SetDefaultBranchCommand command, CancellationToken ct) {
            var branch = await _uow.Organizations.GetBranchByIdAsync(command.BranchId, ct);
            if (branch is null)
                return Result<BranchDto>.NotFound("Branch not found.");

            if (!branch.IsActive)
                return Result<BranchDto>.Failure(
                    "Cannot set an inactive branch as default.", "BRANCH_INACTIVE");

            return await _uow.ExecuteInTransactionAsync(async token =>
            {
                await _uow.Organizations.ClearDefaultBranchAsync(token);
                branch.IsDefault = true;
                _uow.Organizations.UpdateBranch(branch);
                return Result<BranchDto>.Success(new BranchDto
                {
                    Id = branch!.Id,
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
            }, ct);
        }
    }
}
