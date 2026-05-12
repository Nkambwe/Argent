using Argent.Api.Infrastructure.Core.Commands.Access;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Core.Modules.Access.RequestObjects;
using Argent.Api.Infrastructure.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Argent.Api.Controllers {

    [ApiController]
    [Route("api/role-groups")]
    [Authorize]
    [Produces("application/json")]
    public class RoleGroupsController(IMediator mediator, IUserContext userContext) : ControllerBase {
        private readonly IMediator _mediator = mediator;
        private readonly IUserContext _userContext = userContext;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RoleGroupDto>), 200)]
        public async Task<IActionResult> GetAll(CancellationToken ct) {
            var result = await _mediator.Send(new GetRoleGroupsQuery(), ct);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(RoleGroupDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(long id, CancellationToken ct) {
            var result = await _mediator.Send(new GetRoleGroupByIdQuery(id), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        [HttpPost]
        [ProducesResponseType(typeof(RoleGroupDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Create([FromBody] CreateRoleGroupRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new CreateRoleGroupCommand(request.Name, request.Description, request.RoleIds), ct);
            if (!result.IsSuccess)
                return result.ErrorCode == "DUPLICATE_NAME"
                    ? Conflict(new { result.Error })
                    : BadRequest(new { result.Error });

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(RoleGroupDto), 200)]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateRoleGroupRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpdateRoleGroupCommand(id, request.Name, request.Description, request.IsActive), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        [HttpPost("{id:long}/roles")]
        [ProducesResponseType(typeof(RoleGroupDto), 200)]
        public async Task<IActionResult> AssignRoles(long id, [FromBody] AssignRolesToGroupRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new AssignRolesToGroupCommand(id, request.RoleIds), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        [HttpDelete("{id:long}/roles/{roleId:long}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveRole(long id, long roleId, CancellationToken ct) {
            var result = await _mediator.Send(new RemoveRoleFromGroupCommand(id, roleId), ct);
            return result.IsSuccess ? Ok(new { Message = "Role removed from group." })
                : NotFound(new { result.Error });
        }

        [HttpGet("system-policies")]
        [ProducesResponseType(typeof(IEnumerable<SystemPolicyDto>), 200)]
        public async Task<IActionResult> GetSystemPolicies([FromQuery] string? module, CancellationToken ct) {
            var result = await _mediator.Send(new GetSystemPoliciesQuery(module), ct);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(new { result.Error });
        }

        [HttpPut("{id:long}/policy-overrides")]
        [ProducesResponseType(typeof(PolicyOverrideDto), 200)]
        public async Task<IActionResult> SetPolicyOverride(long id, [FromBody] SetPolicyOverrideRequest request, CancellationToken ct) {
            var result = await _mediator.Send(
                new SetPolicyOverrideCommand(id, request.SystemPolicyId, request.OverrideValue, request.Reason), ct);
            return result.IsSuccess ? Ok(result.Data)
                : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error })
                : BadRequest(new { result.Error });
        }

        [HttpDelete("{id:long}/policy-overrides/{policyId:long}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemovePolicyOverride(long id, long policyId, CancellationToken ct) {
            var result = await _mediator.Send(new RemovePolicyOverrideCommand(id, policyId), ct);
            return result.IsSuccess ? Ok(new { Message = "Override removed. System default now applies." })
                : NotFound(new { result.Error });
        }
    }
}
