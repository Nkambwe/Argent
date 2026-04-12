using Argent.Api.Infrastructure.Logging;
using System.Net;
using System.Text.Json;

namespace Argent.Api.Extensions {

    /// <summary>
    /// Global exception middleware that catches anything unhandled and returns a clean JSON error.
    /// This avoids any stack trace from leaking into the clients
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    public class ExceptionMiddleware(RequestDelegate next, IServiceLoggerFactory loggerFactory) {
        private readonly RequestDelegate _next = next;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task InvokeAsync(HttpContext context) {
            try {
                await _next(context);
            } catch (Exception ex) {
                var logger = _loggerFactory.CreateLogger("exceptions");
                logger.Channel = $"EXCEPTION-{context.Request.Path}";
                logger.Log($"{ex.GetType().Name}: {ex.Message}", "ERROR");
                logger.Log(ex.StackTrace ?? "No stack trace", "STACKTRACE");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception) {
            context.Response.ContentType = "application/json";

            var (statusCode, message) = exception switch
            {
                ArgumentNullException => (HttpStatusCode.BadRequest, exception.Message),
                ArgumentException => (HttpStatusCode.BadRequest, exception.Message),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Unauthorized"),
                KeyNotFoundException => (HttpStatusCode.NotFound, exception.Message),
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred")
            };

            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                statusCode = (int)statusCode,
                message,
                traceId = context.TraceIdentifier
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response,
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }
    }
}
