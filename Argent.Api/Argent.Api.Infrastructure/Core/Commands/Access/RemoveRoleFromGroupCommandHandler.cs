using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class RemoveRoleFromGroupCommandHandler(AppDataContext db, IUserContext userContext)
                : IRequestHandler<RemoveRoleFromGroupCommand, Result> {
        private readonly AppDataContext _db = db;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result> Handle(
            RemoveRoleFromGroupCommand command, CancellationToken ct) {
            var member = await _db.RoleGroupMembers
                .FirstOrDefaultAsync(m =>
                    m.RoleGroupId == command.RoleGroupId &&
                    m.RoleId == command.RoleId &&
                    !m.IsDeleted, ct);

            if (member is null)
                return Result.Failure("This role is not assigned to the specified group.", "NOT_FOUND");

            member.IsDeleted = true;
            member.DeletedOn = DateTime.UtcNow;
            member.DeletedBy = _userContext.Username;

            await _db.SaveChangesAsync(ct);
            return Result.Success();
        }
    }

}
