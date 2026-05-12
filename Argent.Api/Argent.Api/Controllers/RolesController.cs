using Argent.Api.Infrastructure.Core.Commands.Access;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Core.Modules.Access.RequestObjects;
using Argent.Api.Infrastructure.Core.Queries.Access;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Argent.Api.Controllers {

    [ApiController]
    [Route("api/roles")]
    [Authorize]
    [Produces("application/json")]
    public class RolesController(IMediator mediator) : ControllerBase {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Summery list of all roles with their permissions count.
        /// </summary>
        [HttpGet("all-roles")]
        [ProducesResponseType(typeof(IEnumerable<RoleSummaryDto>), 200)]
        public async Task<IActionResult> GetRoles() {
            var result = await _mediator.Send(new GetRolesQuery());
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// List all roles with their permissions.
        /// </summary>
        [HttpGet("get-roles-with-permissions")]
        [ProducesResponseType(typeof(IEnumerable<RoleDto>), 200)]
        public async Task<IActionResult> GetRolesWithPermissions() {
            var result = await _mediator.Send(new GetRolesWithPermissionsQuery());
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Get a role with its full permission list.
        /// </summary>
        [HttpGet("get-role/{id:long}")]
        [ProducesResponseType(typeof(RoleDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(long id, CancellationToken ct) {
            var result = await _mediator.Send(new GetRoleByIdQuery(id), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        /// <summary>
        /// Create a new role with optional initial permissions.
        /// </summary>
        [HttpPost("create-role")]
        [ProducesResponseType(typeof(RoleDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Create([FromBody] CreateRoleRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new CreateRoleCommand(request.Name, request.Description, request.PermissionIds), ct);
            if (!result.IsSuccess)
                return result.ErrorCode == "DUPLICATE_NAME"
                    ? Conflict(new { result.Error })
                    : BadRequest(new { result.Error });

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        /// <summary>
        /// Update a role's name, description, or active status. System roles cannot be modified.
        /// </summary>
        [HttpPut("update-role/{id:long}")]
        [ProducesResponseType(typeof(RoleDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateRoleRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpdateRoleCommand(id, request.Name, request.Description), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode is "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Remove a specific role from a user.
        /// </summary>
        [HttpDelete("remove-uer-role/{id:long}/roles/{roleId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveRole(long id, long roleId, CancellationToken ct) {
            var result = await _mediator.Send(new RemoveRoleFromUserCommand(id, roleId), ct);
            return result.IsSuccess ? Ok(new { Message = "Role removed." })
                : NotFound(new { result.Error });
        }

        /// <summary>
        /// Delete a role. Blocked if any users are currently assigned to it.
        /// System roles cannot be deleted.
        /// </summary>
        [HttpDelete("delete-role/{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(long id, CancellationToken ct) {
            var result = await _mediator.Send(new DeleteRoleCommand(id), ct);
            return result.IsSuccess ? Ok(new { Message = "Role deleted." })
                : result.ErrorCode is "SYSTEM_ROLE" or "ROLE_IN_USE"
                    ? BadRequest(new { result.Error })
                    : NotFound(new { result.Error });
        }

        /// <summary>
        /// Assign additional roles to a user. Additive — existing roles are preserved.
        /// </summary>
        [HttpPost("assign-user-role/{id:long}/roles")]
        [ProducesResponseType(typeof(UserDetailDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AssignRoles(long id, [FromBody] AssignRolesToUserRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new AssignRolesToUserCommand(id, request.RoleIds), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// List all available permissions, optionally filtered by module.
        /// </summary>
        [HttpGet("permissions")]
        [ProducesResponseType(typeof(IEnumerable<PermissionDto>), 200)]
        public async Task<IActionResult> GetPermissions([FromQuery] string? module, CancellationToken ct) {
            var result = await _mediator.Send(new GetPermissionsQuery(module), ct);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Assign additional permissions to a role. Additive — existing permissions are preserved.
        /// </summary>
        [HttpPost("assign-role-permissions/{id:long}/permissions")]
        [ProducesResponseType(typeof(RoleDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AssignPermissions(long id, [FromBody] AssignPermissionsRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new AssignPermissionsToRoleCommand(id, request.PermissionIds), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Remove a specific permission from a role.
        /// </summary>
        [HttpDelete("remove-role-permission/{id:long}/permissions/{permissionId:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemovePermission(long id, long permissionId, CancellationToken ct) {
            var result = await _mediator.Send(new RemovePermissionFromRoleCommand(id, permissionId), ct);
            return result.IsSuccess ? Ok(new { Message = "Permission removed." })
                : NotFound(new { result.Error });
        }

    }
}
