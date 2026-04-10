
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;

namespace Argent.Api.Infrastructure.Services {
    public interface IBaseService {
        IServiceLogger Logger { get; }
        IUnitOfWorkFactory UowFactory { get; }
    }

    //_dbContext.Database.CanConnectAsync
    //await EntityFrameworkQueryableExtensions.AnyAsync(_dbContext.Organizations, token);
}
