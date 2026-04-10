
using Argent.Api.Infrastructure.Configuration.Options;
using Argent.Api.Infrastructure.Configuration.Providers;
using Microsoft.Extensions.Options;

namespace Argent.Api.Infrastructure.Logging {
    
    public class ServiceLoggerFactory(ILoggerConfigurationProvider environmentProvider,
                                      IOptions<ServiceLoggingOption> loggingOptions)
                                    : IServiceLoggerFactory {

        private readonly ILoggerConfigurationProvider _environmentProvider = environmentProvider;
        private readonly IOptions<ServiceLoggingOption> _loggingOptions = loggingOptions;

        public IServiceLogger CreateLogger()
            => new ServiceLogger(_environmentProvider, _loggingOptions, _loggingOptions.Value.DefaultLogName);

        public IServiceLogger CreateLogger(string name)
           => new ServiceLogger(_environmentProvider, _loggingOptions, name);
    }

}
