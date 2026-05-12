using Argent.Api.Infrastructure.Core.Commands.Organizations;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Core.Modules.Organization.RequestObjects;
using Argent.Api.Infrastructure.Core.Queries.Organizations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Argent.Api.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrganizationController(IMediator mediator) : ControllerBase {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Get the organization profile with all branches.
        /// </summary>
        [HttpGet("get-organization")]
        [ProducesResponseType(typeof(OrganizationDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(CancellationToken ct) {
            var result = await _mediator.Send(new GetOrganizationQuery(), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        /// <summary>
        /// Get a specific organization by ID.
        /// </summary>
        [HttpGet("get-organization-by-id/{id:long}")]
        [ProducesResponseType(typeof(OrganizationDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(long id, CancellationToken ct) {
            var result = await _mediator.Send(new GetOrganizationQuery(id), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        /// <summary>
        /// Register the organization. Requires a default branch to be included.
        /// Only one organization can exist per deployment.
        /// </summary>
        [HttpPost("create-organization")]
        [ProducesResponseType(typeof(OrganizationDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Create([FromBody] CreateOrganizationRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new CreateOrganizationCommand(request), ct);

            if (!result.IsSuccess) {
                return result.ErrorCode switch
                {
                    "ORGANIZATION_EXISTS" => Conflict(new { result.Error }),
                    "DUPLICATE_REGISTRATION_NUMBER" => Conflict(new { result.Error }),
                    _ => BadRequest(new { result.Error })
                };
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        /// <summary>
        /// Update organization profile. Registration number cannot be changed after creation.
        /// </summary>
        [HttpPut("update-organization/{id:long}")]
        [ProducesResponseType(typeof(OrganizationDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateOrganizationRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpdateOrganizationCommand(id, request), ct);

            return result.IsSuccess ? Ok(result.Data) : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error }) : BadRequest(new { result.Error });
        }

    }

}
