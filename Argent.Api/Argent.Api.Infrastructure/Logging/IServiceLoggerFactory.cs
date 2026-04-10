
namespace Argent.Api.Infrastructure.Logging {
    public interface IServiceLoggerFactory {
        IServiceLogger CreateLogger();
        IServiceLogger CreateLogger(string logName);
    }
}
