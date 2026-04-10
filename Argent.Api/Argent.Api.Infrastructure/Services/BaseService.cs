
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;

namespace Argent.Api.Infrastructure.Services {
    public class BaseService(IServiceLoggerFactory loggerFactory, IUnitOfWorkFactory uowFactory) : IBaseService {
        public IServiceLogger Logger { get; } = loggerFactory.CreateLogger();
        public IUnitOfWorkFactory UowFactory { get; } = uowFactory;
    }
}
