using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organizations {
    public class UpdateBranchCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<UpdateBranchCommand, Result<BranchDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<BranchDto>> Handle(UpdateBranchCommand command, CancellationToken ct) {
            var branch = await _uow.Organizations.GetBranchByIdAsync(command.BranchId, ct);
            if (branch is null)
                return Result<BranchDto>.NotFound("Branch not found.");

            var req = command.Request;

            if (branch.BranchCode != req.BranchCode &&
                await _uow.Organizations.BranchCodeExistsAsync(req.BranchCode, command.BranchId, ct))
                return Result<BranchDto>.Failure(
                    $"Branch code '{req.BranchCode}' is already in use.", "DUPLICATE_CODE");

            branch.BranchCode = req.BranchCode;
            branch.BranchName = req.BranchName;
            branch.Address = req.Address;
            branch.EmailAddress = req.EmailAddress;
            branch.PostalAddress = req.PostalAddress;
            branch.IsActive = req.IsActive;
            branch.UpdatedBy = _userContext.Username;

            _uow.Organizations.UpdateBranch(branch);
            await _uow.CommitAsync(ct);

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
