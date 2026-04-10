using Argent.Api.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Argent.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController(IOrganizationService organization, ILogger<HealthController> logger) : ControllerBase {
        private readonly IOrganizationService _organization = organization;
        private readonly ILogger<HealthController> _logger = logger;

        /// <summary>
        /// Basic liveness check — is the API running?
        /// </summary>
        [HttpGet("ping")]
        public IActionResult Ping()
            => Ok(new { status = "alive", timestamp = DateTime.UtcNow, version = "1.0.0" });

        /// <summary>
        /// Full health check:(1)check whether we can reach the database; (2)check whether the company is created
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetHealth() {
            var checks = new Dictionary<string, object>();
            var isHealthy = true;

            //..check connectivity
            try {
                
                var canConnect = await _organization.CanConnectAsync();
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

            try {
                var hasOrg = await _organization.IsOrganizationSetup();
                checks["organization"] = new
                {
                    status = hasOrg ? "healthy" : "not_initialized",
                    hasOrganization = hasOrg,
                    message = hasOrg ? "Organization record exists" : "No organization found — run setup"
                };
                if (!hasOrg) isHealthy = false;
            } catch (Exception ex) {
                checks["organization"] = new { status = "error", error = ex.Message };
                isHealthy = false;
            }

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
