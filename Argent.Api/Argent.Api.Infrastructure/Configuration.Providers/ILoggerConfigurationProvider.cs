namespace Argent.Api.Infrastructure.Configuration.Providers {
    public interface ILoggerConfigurationProvider {
        bool IsLive { get; }
        string RootLogPath { get; }
    }
}
