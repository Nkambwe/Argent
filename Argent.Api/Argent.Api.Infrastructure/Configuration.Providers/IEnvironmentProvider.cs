namespace Argent.Api.Infrastructure.Configuration.Providers {
    public interface IEnvironmentProvider {
        bool IsLive { get; }
    }
}
