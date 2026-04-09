using Argent.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Argent.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController(AppDataContext dbContext, ILogger<HealthController> logger) : ControllerBase {
        private readonly AppDataContext _dbContext = dbContext;
        private readonly ILogger<HealthController> _logger = logger;

        /// <summary>
        /// Basic liveness check — is the API running?
        /// </summary>
        [HttpGet("ping")]
        public IActionResult Ping()
            => Ok(new { status = "alive", timestamp = DateTime.UtcNow, version = "1.0.0" });

        /// <summary>
        /// Full health check:
        /// check whether we can reach the database
        /// check whether the company is created
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetHealth(CancellationToken ct) {
            var checks = new Dictionary<string, object>();
            var isHealthy = true;

            //..check connectivity
            try {
                var canConnect = await _dbContext.Database.CanConnectAsync(ct);
                checks["database"] = new { status = canConnect ? "healthy" : "unhealthy", canConnect };
                if (!canConnect) isHealthy = false;
            } catch (Exception ex) {
                _logger.LogWarning(ex, "Database health check failed");
                checks["database"] = new { status = "unhealthy", error = ex.Message };
                isHealthy = false;
            }

            //..check system initialization
            checks["organization"] = new {
                status = "pending",
                message = "Organization module not yet initialized"
            };

            // Uncomment once Organization entity is set up:
            // try
            // {
            //     var hasOrg = await _dbContext.Organizations.AnyAsync(ct);
            //     checks["organization"] = new
            //     {
            //         status = hasOrg ? "healthy" : "not_initialized",
            //         hasOrganization = hasOrg,
            //         message = hasOrg ? "Organization record exists" : "No organization found — run setup"
            //     };
            //     if (!hasOrg) isHealthy = false;
            // }
            // catch (Exception ex)
            // {
            //     checks["organization"] = new { status = "error", error = ex.Message };
            //     isHealthy = false;
            // }

            var statusCode = isHealthy ? 200 : 503;
            var response = new
            {
                status = isHealthy ? "healthy" : "degraded",
                timestamp = DateTime.UtcNow,
                checks
            };

            return StatusCode(statusCode, response);
        }
    }
}
