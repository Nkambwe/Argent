using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class RemoveRoleFromUserCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<RemoveRoleFromUserCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result> Handle(RemoveRoleFromUserCommand command, CancellationToken ct) {
            var userRole = await _uow.Users.GetUserRoleAsync(command.UserId, command.RoleId, ct);
            if (userRole is null || userRole.IsDeleted)
                return Result.Failure("This role is not assigned to the user.", "NOT_FOUND");

            userRole.DeletedBy = _userContext.Username;
            _uow.Users.RemoveUserRole(userRole);
            await _uow.CommitAsync(ct);
            return Result.Success();
        }
    }


}
