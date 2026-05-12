using Argent.Api.Domain.Entities;
using Argent.Api.Infrastructure.Data;

namespace Argent.Api.Infrastructure.Repositories {
    public class BranchRepository(AppDataContext context) 
        : Repository<Branch>(context), IBranchRepository {
    }
}
