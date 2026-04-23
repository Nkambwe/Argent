using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Data;

namespace Argent.Api.Infrastructure.Repositories.Access {
    public class RoleGroupRepository(AppDataContext context) :Repository<RoleGroup>(context), IRoleGroupRepository {
        private readonly AppDataContext _context = context;

    }

}
