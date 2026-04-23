using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Data;

namespace Argent.Api.Infrastructure.Repositories.Access {
    public class RoleGroupMemberRepository(AppDataContext context) : Repository<RoleGroupMember>(context), IRoleGroupMemberRepository {
        private readonly AppDataContext _context = context;

    }

}
