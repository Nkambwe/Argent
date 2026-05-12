using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class RemovePermissionFromRoleCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<RemovePermissionFromRoleCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result> Handle(
            RemovePermissionFromRoleCommand command, CancellationToken ct) {
            await _uow.Roles.RemovePermissionAsync(command.RoleId, command.PermissionId, ct);
            await _uow.CommitAsync(ct);
            return Result.Success();
        }
    }

}
