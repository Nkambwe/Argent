using Argent.Api.Infrastructure.Logging;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Common.Behaviour {
    /// <summary>
    /// Logs every MediatR request with execution time.
    /// Runs first in the pipeline so it captures total elapsed time including
    /// validation, auth checks, and handler execution.
    /// </summary>
    public class LoggingPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull {
        private readonly IServiceLoggerFactory _loggerFactory;

        public LoggingPipelineBehavior(IServiceLoggerFactory loggerFactory) {
            _loggerFactory = loggerFactory;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken ct) {
            var requestName = typeof(TRequest).Name;
            var logger = _loggerFactory.CreateLogger("pipeline");
            logger.Channel = $"MEDIATOR-{requestName}";

            logger.Log($"Handling {requestName}", "REQUEST");

            var sw = System.Diagnostics.Stopwatch.StartNew();
            try {
                var response = await next();
                sw.Stop();
                logger.Log($"Handled {requestName} in {sw.ElapsedMilliseconds}ms", "RESPONSE");
                return response;
            }
            catch (Exception ex) {
                sw.Stop();
                logger.Log($"Error handling {requestName} after {sw.ElapsedMilliseconds}ms: {ex.Message}", "ERROR");
                throw;
            }
        }
    }
}
