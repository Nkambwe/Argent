using Microsoft.Extensions.Logging;

namespace Argent.Api.Infrastructure.Logging {
    public class ServiceLoggerAdapter : ILogger {
        private readonly IServiceLogger _logger;
        private readonly string _category;

        public ServiceLoggerAdapter(IServiceLogger logger, string category) {
            _logger = logger;
            _category = category;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
            => new LoggerScope(state, _logger);

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter) {
            if (!IsEnabled(logLevel)) return;

            var message = formatter(state, exception);
            if (exception is not null)
                message += Environment.NewLine + exception;

            _logger.Log(message, logLevel.ToString().ToUpperInvariant());
        }

        private sealed class LoggerScope : IDisposable {
            private readonly IServiceLogger _logger;

            public LoggerScope(object? state, IServiceLogger logger) {
                _logger = logger;

                if (state is IEnumerable<KeyValuePair<string, object?>> dict) {
                    foreach (var kv in dict) {
                        if (kv.Key == "Channel") _logger.Channel = kv.Value?.ToString() ?? "";
                        if (kv.Key == "Id") _logger.Id = kv.Value?.ToString() ?? "";
                    }
                }
            }

            public void Dispose() { }
        }
    }
}
