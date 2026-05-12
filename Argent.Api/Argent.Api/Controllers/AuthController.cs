using Argent.Api.Infrastructure.Core.Commands.Access;
using Argent.Api.Infrastructure.Core.Modules.Access;
using Argent.Api.Infrastructure.Core.Modules.Access.RequestObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Argent.Api.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController(IMediator mediator) : ControllerBase {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Authenticate with username and password.
        /// Returns a JWT access token and a refresh token.
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct) {
            var ip = Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? HttpContext.Connection.RemoteIpAddress?.ToString();
            var result = await _mediator.Send(new LoginCommand(request.Username, request.Password, ip), ct);
            return result.IsSuccess ? Ok(result.Data) : result.ErrorCode switch
            {
                "ACCOUNT_LOCKED" => StatusCode(423, new { result.Error }),
                "INVALID_CREDENTIALS" => Unauthorized(new { result.Error }),
                _ => BadRequest(new { result.Error })
            };
        }

        /// <summary>
        /// Exchange a valid refresh token for a new access token and rotated refresh token.
        /// </summary>
        [HttpPost("refresh")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthResponse), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken ct) {
            var ip = Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? HttpContext.Connection.RemoteIpAddress?.ToString();
            var result = await _mediator.Send(new RefreshTokenCommand(request.AccessToken, request.RefreshToken, ip), ct);

            return result.IsSuccess ? Ok(result.Data) : Unauthorized(new { result.Error });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request, CancellationToken token) {
            var ip = Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            var userId = long.Parse(userIdClaim);
            var result = await _mediator.Send(new LogoutCommand(request.RefreshToken, ip, userId), token);
            return result.IsSuccess ? Ok(new { message = "Logged out successfully" }) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Change the authenticated user's own password.
        /// </summary>
        [HttpPost("change-password")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request, CancellationToken ct) {
            var userId = GetCurrentUserId();
            if (userId == 0)
                return Unauthorized();

            var result = await _mediator.Send(new ChangePasswordCommand(userId, request), ct);
            return result.IsSuccess ? Ok(new { Message = "Password changed successfully." })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Returns the calling user's resolved context from the JWT.
        /// </summary>
        [HttpGet("me")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public IActionResult Me() {
            return Ok(new
            {
                userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,
                username = User.FindFirst("username")?.Value,
                fullName = User.FindFirst("fullname")?.Value,
                homeBranchId = User.FindFirst("homebranch")?.Value,
                permissions = User.FindFirst("permissions")?.Value?.Split(',') ?? []
            });
        }

        private long GetCurrentUserId() {
            var claim = User.FindFirst("userId")?.Value;
            return long.TryParse(claim, out var id) ? id : 0;
        }
    }
}
