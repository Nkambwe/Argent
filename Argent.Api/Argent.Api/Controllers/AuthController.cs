using Argent.Api.Infrastructure.Core.Commands.Access;
using Argent.Api.Infrastructure.Core.Modules.Access;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
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
            var result = await _mediator.Send(new RefreshTokenCommand(request.RefreshToken, ip), ct);
            return result.IsSuccess ? Ok(result.Data) : Unauthorized(new { result.Error });
        }

        /// <summary>
        /// Create a new system user. Requires SystemAdmin permission.
        /// </summary>
        [HttpPost("users")]
        [Authorize]
        [ProducesResponseType(typeof(UserDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new CreateUserCommand(
                request.Username,
                request.Email,
                request.Password,
                request.FirstName,
                request.LastName,
                request.PhoneNumber,
                request.DefualtBranchId,
                request.RoleIds), ct);

            return result.IsSuccess ? StatusCode(201, result.Data) : result.ErrorCode switch {
                "DUPLICATE_USERNAME" => Conflict(new { result.Error }),
                "DUPLICATE_EMAIL" => Conflict(new { result.Error }),
                "NOT_FOUND" => NotFound(new { result.Error }),
                _ => BadRequest(new { result.Error })
            };
        }
    }
}
