using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Data;

namespace Argent.Api.Infrastructure.Repositories.Access {
    public class RoleRepository(AppDataContext context) : Repository<Role>(context), IRoleRepository {
        private readonly AppDataContext _context = context;
    }

}
