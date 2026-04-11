using Argent.Api.Infrastructure.Core.Commands.Organization;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Core.Modules.Organization.RequestObjects;
using Argent.Api.Infrastructure.Core.Queries;
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
        [HttpGet]
        [ProducesResponseType(typeof(OrganizationDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(CancellationToken ct) {
            var result = await _mediator.Send(new GetOrganizationQuery(), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        /// <summary>
        /// Get a specific organization by ID.
        /// </summary>
        [HttpGet("{id:long}")]
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
        [HttpPost]
        [ProducesResponseType(typeof(OrganizationDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Create([FromBody] OrganizationCreateRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new CreateOrganizationCommand(
                request.RegisteredName,
                request.ShortName,
                request.RegistrationNumber,
                request.BusinessLine,
                request.ContactEmail,
                request.DefaultBranch.BranchCode,
                request.DefaultBranch.BranchName,
                request.DefaultBranch.Address,
                request.DefaultBranch.EmailAddress,
                request.DefaultBranch.PostalAddress
            ), ct);

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
        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(OrganizationDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(long id, [FromBody] OrganizationUpdateRequest request, CancellationToken ct) {
            var result = await _mediator.Send(new UpdateOrganizationCommand(
                id,
                request.RegisteredName,
                request.ShortName,
                request.BusinessLine,
                request.ContactEmail
            ), ct);

            return result.IsSuccess ? Ok(result.Data) : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error }) : BadRequest(new { result.Error });
        }

        /// <summary>
        /// Get all branches for this organization. Default branch is always listed first.
        /// </summary>
        [HttpGet("{organizationId:long}/branches")]
        [ProducesResponseType(typeof(IEnumerable<BranchDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBranches(long organizationId, CancellationToken ct) {
            var result = await _mediator.Send(new GetBranchesQuery(organizationId), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        /// <summary>
        /// Get a specific branch by ID.
        /// </summary>
        [HttpGet("{organizationId:long}/branches/{branchId:long}")]
        [ProducesResponseType(typeof(BranchDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBranch(long organizationId, long branchId, CancellationToken ct) {
            var result = await _mediator.Send(new GetBranchByIdQuery(organizationId, branchId), ct);
            return result.IsSuccess ? Ok(result.Data) : NotFound(new { result.Error });
        }

        /// <summary>
        /// Add a new branch to the organization.
        /// </summary>
        [HttpPost("{organizationId:long}/branches")]
        [ProducesResponseType(typeof(BranchDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> CreateBranch(long organizationId, [FromBody] BranchCreateRequest request,
            CancellationToken ct) {
            var result = await _mediator.Send(new CreateBranchCommand(
                organizationId,
                request.BranchCode,
                request.BranchName,
                request.Address,
                request.EmailAddress,
                request.PostalAddress,
                MakeDefault: false 
            ), ct);

            if (!result.IsSuccess) {
                return result.ErrorCode switch
                {
                    "NOT_FOUND" => NotFound(new { result.Error }),
                    "DUPLICATE_BRANCH_NAME" => Conflict(new { result.Error }),
                    _ => BadRequest(new { result.Error })
                };
            }

            return CreatedAtAction(nameof(GetBranch), new { organizationId, branchId = result.Data!.Id }, result.Data);
        }

        /// <summary>
        /// Promote a branch to the default. The previous default is automatically demoted.
        /// This operation is atomic — both changes commit together or neither does.
        /// </summary>
        [HttpPatch("{organizationId:long}/branches/{branchId:long}/set-default")]
        [ProducesResponseType(typeof(BranchDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> SetDefaultBranch(long organizationId, long branchId, CancellationToken ct) {
            var result = await _mediator.Send(new SetDefaultBranchCommand(organizationId, branchId), ct);
            return result.IsSuccess ? Ok(result.Data) : result.ErrorCode == "NOT_FOUND" ? NotFound(new { result.Error }) : BadRequest(new { result.Error });
        }
    }

}
