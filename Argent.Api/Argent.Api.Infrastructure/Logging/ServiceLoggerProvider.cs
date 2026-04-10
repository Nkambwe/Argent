
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Argent.Api.Infrastructure.Logging {
    public class ServiceLoggerProvider(IServiceScopeFactory factory) : ILoggerProvider {
        private readonly IServiceScopeFactory _factory = factory ?? throw new ArgumentNullException(nameof(factory));

        public ILogger CreateLogger(string categoryName) {
            //..create a scope to resolve scoped dependencies
            var scope = _factory.CreateScope();
            var loggerFactory = scope.ServiceProvider.GetRequiredService<IServiceLoggerFactory>();

            //..log to a single file in appsttings config
            return new ServiceLoggerAdapter(loggerFactory.CreateLogger(), categoryName);
        }

        public void Dispose() { }
    }
}
