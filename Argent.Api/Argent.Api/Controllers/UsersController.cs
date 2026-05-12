using Argent.Api.Infrastructure.Core.Commands.Access;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Core.Modules.Access.RequestObjects;
using Argent.Api.Infrastructure.Core.Queries.Access;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Argent.Api.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class UsersController(IMediator mediator, IUserContext userContext) : ControllerBase {
        private readonly IMediator _mediator = mediator;
        private readonly IUserContext _userContext = userContext;

        #region Users
        /// <summary>
        /// List all users with optional filtering by search term, branch, or active status.
        /// </summary>
        [HttpGet("get-users")]
        [ProducesResponseType(typeof(PagedResult<UserSummaryDto>), 200)]
        public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] long? branchId,
            [FromQuery] bool? isActive, [FromQuery] int page = 1, [FromQuery] int pageSize = 20,
            CancellationToken ct = default) {
            var result = await _mediator.Send(new GetUsersQuery(search, branchId, isActive, page, pageSize), ct);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Get a user by ID including their roles and branch access.
        /// </summary>
        [HttpGet("get-user/{id:long}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(long id, CancellationToken ct) {
            var result = await _mediator.Send(new GetUserByIdQuery(id), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        /// <summary>
        /// Create a new system user. Requires Access.CreateUser permission.
        /// The user is automatically granted access to their home branch.
        /// </summary>
        [HttpPost("create-user")]
        [ProducesResponseType(typeof(UserDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new CreateUserCommand(request), ct);

            if (!result.IsSuccess) {
                return result.ErrorCode switch
                {
                    "DUPLICATE_USERNAME" or "DUPLICATE_EMAIL" => Conflict(new { result.Error }),
                    "NOT_FOUND" => NotFound(new { result.Error }),
                    _ => BadRequest(new { result.Error })
                };
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        /// <summary>
        /// Update a user's profile details.
        /// </summary>
        [HttpPut("update-user/{id:long}")]
        [ProducesResponseType(typeof(UserDetailDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateUserRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpdateUserCommand(id, request), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Deactivate by soft-deleting a user. Their audit history is preserved.
        /// The system admin account and your own account cannot be deactivated.
        /// </summary>
        [HttpDelete("deactivate-user/{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Deactivate(long id, CancellationToken ct) {
            var result = await _mediator.Send(new DeactivateUserCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "User deactivated." })
                : result.ErrorCode is "PROTECTED" or "SELF_DEACTIVATE"
                    ? BadRequest(new { result.Error })
                    : NotFound(new { result.Error });
        }

        #endregion

        #region Branch Access
        /// <summary>
        /// Grant a user access to an additional branch.
        /// </summary>
        [HttpPost("assign-branches/{id:long}")]
        [ProducesResponseType(typeof(UserDetailDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AssignBranch(long id, [FromBody] AssignBranchAccessRequest request, CancellationToken ct) {
            var result = await _mediator.Send(
                new AssignBranchAccessCommand(id, request.BranchId, request.CanPost), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Update the CanPost flag for a user's existing branch access.
        /// </summary>
        [HttpPut("update-branch-bccess/{id:long}/branches/{branchId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateBranchAccess(long id, long branchId, [FromBody] UpdateBranchAccessRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpdateBranchAccessCommand(id, branchId, request.CanPost), ct);
            return result.IsSuccess ? Ok(new { Message = "Branch access updated." })
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Revoke a user's access to a branch.
        /// Cannot remove access to the user's home branch.
        /// </summary>
        [HttpDelete("{id:long}/branches/{branchId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveBranchAccess(long id, long branchId, CancellationToken ct) {
            var result = await _mediator.Send(new RemoveBranchAccessCommand(id, branchId), ct);
            return result.IsSuccess ? Ok(new { Message = "Branch access revoked." })
                : result.ErrorCode == "DEFAULT_BRANCH" ? BadRequest(new { result.Error })
                : NotFound(new { result.Error });
        }

        #endregion
    }
}
