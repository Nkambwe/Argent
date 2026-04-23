using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class RemovePolicyOverrideCommandHandler(AppDataContext db, IUserContext userContext)
                : IRequestHandler<RemovePolicyOverrideCommand, Result> {
        private readonly AppDataContext _db = db;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result> Handle(RemovePolicyOverrideCommand command, CancellationToken ct) {
            var override_ = await _db.RoleGroupPolicyOverrides
                .FirstOrDefaultAsync(o =>
                    o.RoleGroupId == command.RoleGroupId &&
                    o.SystemPolicyId == command.SystemPolicyId &&
                    !o.IsDeleted, ct);

            if (override_ is null)
                return Result.Failure("Override not found for this group and policy.", "NOT_FOUND");

            override_.IsDeleted = true;
            override_.DeletedOn = DateTime.UtcNow;
            override_.DeletedBy = _userContext.Username;

            await _db.SaveChangesAsync(ct);
            return Result.Success();
        }
    }

}
