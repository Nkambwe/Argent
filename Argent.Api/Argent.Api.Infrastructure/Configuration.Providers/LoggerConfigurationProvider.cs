using Microsoft.Extensions.Configuration;

namespace Argent.Api.Infrastructure.Configuration.Providers {

    public class LoggerConfigurationProvider: ILoggerConfigurationProvider {

        public bool IsLive { get; }
        public string RootLogPath { get; }

        public LoggerConfigurationProvider(IConfiguration configuration) {
            IsLive = configuration.GetValue<bool>("EnvironmentOptions:IsLive");
            var configured = configuration.GetValue<string>("EnvironmentOptions:LogRootPath");

            //..fall back to a sensible default per OS if not configured
            RootLogPath = !string.IsNullOrWhiteSpace(configured) ? configured : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "mfiLogs");
        }

    }
}
