using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class DeleteRoleCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<DeleteRoleCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result> Handle(DeleteRoleCommand command, CancellationToken ct) {
            var role = await _uow.Roles.GetByIdAsync(command.RoleId, ct);
            if (role is null)
                return Result.Failure("Role not found.", "NOT_FOUND");

            if (role.IsSystemRole)
                return Result.Failure(
                    "System roles cannot be deleted.", "SYSTEM_ROLE");

            // Block deletion if any users currently hold this role
            var activeAssignments = await _uow.Users.CountActiveRoleAssignmentsAsync(command.RoleId, ct);
            if (activeAssignments > 0)
                return Result.Failure($"Cannot delete role — {activeAssignments} user(s) currently assigned to it. " +
                    "Remove all assignments first.", "ROLE_IN_USE");

            role.IsDeleted = true;
            role.DeletedOn = DateTime.UtcNow;
            role.DeletedBy = _userContext.Username;
            _uow.Roles.Update(role);
            await _uow.CommitAsync(ct);

            return Result.Success();
        }
    }

}
